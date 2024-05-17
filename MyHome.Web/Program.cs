using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;
using Serilog;
using Serilog.Sinks.Graylog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<ApplicationDbContext>(options => options
    .UseSqlite(builder.Configuration.GetConnectionString("Sqlite")))
    .AddIdentity<ApplicationUser, IdentityRole>(options => {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 4;
    })
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

// Serilog
builder.Host.UseSerilog((context, services, configuration) => {
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
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();