using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyHome.Data;
using MyHome.Data.Authorize;
using MyHome.Data.Families;
using MyHome.Data.Homes;
using MyHome.Data.Users;
using MyHome.Shared;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
                    builder.Configuration["Security:SecretKey"] ?? "")),
            ValidAudience = builder.Configuration["Security:Audience"],
            ValidIssuer = builder.Configuration["Security:Issuer"],
            RequireExpirationTime = true,
            RequireAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateAudience = true,
        };
    });
builder.Services.Configure<SecurityModel>(builder.Configuration.GetSection("Security"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<IUserService<ApplicationUser>, DbUserService>();
builder.Services.AddScoped<ITokenCreator<ApplicationUser>, JwtTokenCreator>();
builder.Services.AddScoped<IFamilyService, DbFamilyService>();
builder.Services.AddScoped<IHomeService, DbHomeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
