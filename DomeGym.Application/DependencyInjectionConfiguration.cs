using Microsoft.Extensions.DependencyInjection;

namespace DomeGym.Application;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjectionConfiguration));
        });
        return services;
    }
}