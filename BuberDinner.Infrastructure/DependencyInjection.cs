using System.Text;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Application.Common.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistance;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Persistence.Interceptors;
using BuberDinner.Infrastructure.Persistence.Repositories;
using BuberDinner.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
        .AddAuth(configuration)
        .AddPersistance();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        services.AddDbContext<BuberDinnerDbContext>(options =>
            options.UseSqlServer("Server=DESKTOP-4AFAF05;Database=BuberDinner;integrated security=true;TrustServerCertificate=True"));

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configration)
    {
        var jwtSettings = new JwtSettings();
        configration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJWTTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

        return services;
    }
}