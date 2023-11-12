using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using ErrorOr;
using MediatR;
using DomeGym.Domain.Common.Constants;
using DomeGym.Application.Common.Interfaces;

namespace DomeGym.Application.Subscription.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler
    : IRequestHandler<CreateSubscriptionCommand, ErrorOr<SubscriptionEntity>>
{
    private readonly ISubscriptionRespository _subscriptionRespository;

    public CreateSubscriptionCommandHandler(ISubscriptionRespository subscriptionRespository)
    {
        _subscriptionRespository = subscriptionRespository;
    }

    public async Task<ErrorOr<SubscriptionEntity>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        // TODO: temporarily put the "Free subscription"
        var subscriptionToSave = new SubscriptionEntity(DomainConstants.FreeSubscription);

        // TODO: inside "CreateSubscriptionAsync" implement validation on DomainKey
        await _subscriptionRespository.CreateSubscriptionAsync(subscriptionToSave);

        return subscriptionToSave;
    }
}