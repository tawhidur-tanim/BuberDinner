using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Application.Common.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistance;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, ConfigurationManager configuration)
    {
        serviceCollection.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        serviceCollection.AddSingleton<IJWTTokenGenerator, JwtTokenGenerator>();
        serviceCollection.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        serviceCollection.AddSingleton<IUserRepository, UserRepository>();

        return serviceCollection;
    }
}