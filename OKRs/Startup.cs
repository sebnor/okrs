using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OKRs.Models;
using OKRs.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using OKRs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using AspNetCore.Identity.DocumentDb;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;

namespace OKRs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // User secrets are loaded automatically when app runs in Development mode
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add DocumentDb client singleton instance
            services.AddSingleton<IDocumentClient>(new DocumentClient(new Uri(Configuration["Database:HostUrl"]), Configuration["Database:Password"]));

            services.AddIdentity<ApplicationUser, DocumentDbIdentityRole>()
            .AddDocumentDbStores(options =>
            {
                options.Database = Configuration["Database:Name"];
                options.UserStoreDocumentCollection = Configuration["Database:UserCollection"];
            });

            services.Configure<DataConfiguration>(options => Configuration.GetSection("Database").Bind(options));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IObjectivesRepository, ObjectivesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
            services.AddScoped<ICurrentContext, CurrentContext>();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                googleOptions.Events = new OAuthEvents
                {
                    OnCreatingTicket = context =>
                    {
                        var domainFilter = Configuration["Authentication:Google:DomainFilter"];
                        string domain = context.User.Value<string>("domain");
                        if (!string.IsNullOrEmpty(domainFilter) && domain != domainFilter)
                            throw new GoogleAuthenticationException($"You must sign in with a {domainFilter} email address");

                        return Task.CompletedTask;
                    }
                };
            });

            Task.Run(async () => await SetupAzureDocumentDBAsync());

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        private async Task SetupAzureDocumentDBAsync()
        {
            using (var client = new DocumentClient(new Uri(Configuration["Database:HostUrl"]), Configuration["Database:Password"]))
            {
                //Create Database if it doesn't exists
                await client.CreateDatabaseIfNotExistsAsync(new Database { Id = Configuration["Database:Name"] });

                //Create user collection
                await client.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(Configuration["Database:Name"]),
                    new DocumentCollection { Id = Configuration["Database:UserCollection"] },
                    new RequestOptions { OfferThroughput = 400 });

                //Create objectives collection
                await client.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(Configuration["Database:Name"]),
                    new DocumentCollection { Id = Configuration["Database:ObjectivesCollection"] },
                    new RequestOptions { OfferThroughput = 400 });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
