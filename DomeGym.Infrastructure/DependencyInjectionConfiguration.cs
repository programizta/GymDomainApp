namespace DomeGym.Infrastructure;

using DomeGym.Application.Common.Interfaces;
using DomeGym.Infrastructure.Common.Persistence;
using DomeGym.Infrastructure.Subscription.Persistence;
using Microsoft.EntityFrameworkCore;
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
        services.AddDbContext<DomeGymDbContext>(options => options.UseSqlite("Data Source = DomeGym.db"));
        services.AddScoped(typeof(ISubscriptionRespository), typeof(SubscriptionRepository));
        return services;
    }
}