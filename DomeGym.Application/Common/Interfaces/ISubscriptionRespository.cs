using DomeGym.Domain.SubscriptionAggregate;
using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;

namespace DomeGym.Application.Common.Interfaces;

public interface ISubscriptionRespository
{
    Task AddSubscriptionAsync(SubscriptionEntity subscriptionEntity);

    Task<SubscriptionEntity?> GetSubscriptionByIdAsync(Guid subscriptionId);

    Task<SubscriptionDetails?> GetSubscriptionDetailsByName(string subscriptionName);
}