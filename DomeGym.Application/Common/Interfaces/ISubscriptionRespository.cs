using DomeGym.Domain.SubscriptionAggregate;
using ErrorOr;
using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;

namespace DomeGym.Application.Common.Interfaces;

public interface ISubscriptionRespository
{
    Task AddSubscriptionAsync(SubscriptionEntity subscriptionEntity);

    Task<SubscriptionEntity?> GetSubscriptionByIdAsync(Guid subscriptionId);

    Task<SubscriptionDetails?> GetSubscriptionDetailsByName(string subscriptionName);

    Task<bool> DeleteSubscriptionByIdAsync(Guid subscriptionId);
}