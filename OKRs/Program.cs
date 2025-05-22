using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OKRs.Data;
using OKRs;
using OKRs.Models;
using OKRs.Repositories;
using OKRs.Services;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OKRsConfiguration>(builder.Configuration.GetSection("Application"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ObjectivesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ProgramVersion, ProgramVersion>();
builder.Services.AddScoped<IObjectivesRepository, ObjectivesRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
builder.Services.AddScoped<ICurrentContext, CurrentContext>();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    googleOptions.Events = new OAuthEvents
    {
        OnCreatingTicket = context =>
        {
            var domainFilter = builder.Configuration["Authentication:Google:DomainFilter"];
            string domain = context.User.GetString("domain");
            if (!string.IsNullOrEmpty(domainFilter) && domain != domainFilter)
                throw new GoogleAuthenticationException($"You must sign in with an {domainFilter} email address. You signed in with a {domain} email address.");

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    TelemetryDebugWriter.IsTracingDisabled = true;
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
