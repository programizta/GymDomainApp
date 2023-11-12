using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;

namespace DomeGym.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}