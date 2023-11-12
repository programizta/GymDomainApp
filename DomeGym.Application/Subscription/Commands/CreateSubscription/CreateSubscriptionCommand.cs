using ErrorOr;
using MediatR;
using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;

namespace DomeGym.Application.Subscription.Commands.CreateSubscription;

public record CreateSubscriptionCommand(string SubscriptionType, Guid AdminId)
    : IRequest<ErrorOr<SubscriptionEntity>>;