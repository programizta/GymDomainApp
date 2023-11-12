namespace DomeGym.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionConfiguration
{
    /// <summary>
    /// TODO: because we must add the reference of this project to the DomeGym.Api to register all services properly
    /// but we don't want to
    /// reference any other symbols but this DI container registration, make all the classes and methods internal.
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        return services;
    }
}