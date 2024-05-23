using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyHome.Web.Components;
using MyHome.Web.Components.Account;
using MyHome.Web.Data;
using MyHome.Web.Services;
using Serilog;
using Serilog.Sinks.Graylog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options
                .UseSqlite(builder.Configuration.GetConnectionString("Sqlite")))
                .AddQuickGridEntityFrameworkAdapter();
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                }).AddIdentityCookies();
builder.Services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 4;
                }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IHouseService, HouseService>();
builder.Services.AddScoped<IMeterReadingService, MeterReadingService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();

// Serilog
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.WriteTo.Graylog(new GraylogSinkOptions
    {
        HostnameOrAddress = builder.Configuration["Graylog:HostnameOrAddress"],
        Port = Convert.ToInt32(builder.Configuration["Graylog:Port"]),
        TransportType = Serilog.Sinks.Graylog.Core.Transport.TransportType.Tcp,
        Facility = AppDomain.CurrentDomain.FriendlyName,
        MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information
    }).Enrich.FromLogContext();
});

var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseHsts();
    app.UseExceptionHandler("/Error");
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();
app.MapAdditionalIdentityEndpoints();
app.Run();