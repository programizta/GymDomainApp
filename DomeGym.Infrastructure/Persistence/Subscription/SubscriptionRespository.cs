using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using DomeGym.Application.Common.Interfaces;

namespace DomeGym.Infrastructure.Persistence.Subscription;

public class SubscriptionRepository : ISubscriptionRespository
{
    public Task CreateSubscriptionAsync(SubscriptionEntity subscriptionEntity)
    {
        return Task.CompletedTask;
    }
}