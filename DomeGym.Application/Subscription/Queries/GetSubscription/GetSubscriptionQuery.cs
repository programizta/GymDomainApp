using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Subscription.Queries.GetSubscription;

public record GetSubscriptionQuery(Guid SubscriptionId) : IRequest<ErrorOr<SubscriptionEntity>>;