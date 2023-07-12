using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IJWTTokenGenerator, JwtTokenGenerator>();

        return serviceCollection;
    }
}