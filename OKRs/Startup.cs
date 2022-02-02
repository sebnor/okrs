using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OKRs.Web.Data;
using OKRs.Web.Models;
using OKRs.Web.Repositories;
using OKRs.Web.Services;
using System.Threading.Tasks;

namespace OKRs.Web
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
            // services.AddOptions();
            services.Configure<OKRsConfiguration>(Configuration.GetSection("Application"));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ObjectivesDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ProgramVersion, ProgramVersion>();
            services.AddScoped<IObjectivesRepository, ObjectivesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
            services.AddScoped<ICurrentContext, CurrentContext>();
            services.AddApplicationInsightsTelemetry();
            services.AddSingleton<ITextSerializer, JsonTextSerializer>();

            services.AddTransient<IEmailSender, EmailSender>(); //Add sendgrid service for emails
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                googleOptions.Events = new OAuthEvents
                {
                    OnCreatingTicket = context =>
                    {
                        var domainFilter = Configuration["Authentication:Google:DomainFilter"];
                        string domain = context.User.GetString("domain");
                        if (!string.IsNullOrEmpty(domainFilter) && domain != domainFilter)
                            throw new GoogleAuthenticationException($"You must sign in with a {domainFilter} email address");

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddMvc(config =>
            {
                config.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
                //app.UseDatabaseErrorPage();
                TelemetryDebugWriter.IsTracingDisabled = true; // Avoid having App insights flooding the debug console
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
