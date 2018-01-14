using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OKRs.Data;
using OKRs.Models;
using OKRs.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using OKRs.Repositories;
using Microsoft.AspNetCore.Http;

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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.Configure<AppConfiguration>(options => Configuration.Bind(options));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IObjectivesRepository, ObjectivesRepository>();
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
                        string domain = context.User.Value<string>("domain");
                        if (domain != "ifacts.se")
                            throw new GoogleAuthenticationException("You must sign in with a ifacts.se email address");

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddMvc();
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
