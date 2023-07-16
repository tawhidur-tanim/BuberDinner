using BuberDinner.Application.Service.Authentication;
using BuberDinner.Application.Service.Authentication.Commands;
using BuberDinner.Application.Service.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        serviceCollection.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();

        return serviceCollection;
    }
}