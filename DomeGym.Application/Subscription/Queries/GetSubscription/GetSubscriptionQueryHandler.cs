using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using ErrorOr;
using MediatR;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.SubscriptionAggregate;

namespace DomeGym.Application.Subscription.Queries.GetSubscription;

public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<SubscriptionEntity>>
{
    private readonly ISubscriptionRespository _subscriptionRespository;

    //private readonly IUnitOfWork _unitOfWork;

    public GetSubscriptionQueryHandler(ISubscriptionRespository subscriptionRespository/*, IUnitOfWork unitOfWork*/)
    {
        _subscriptionRespository = subscriptionRespository;
        //_unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<SubscriptionEntity>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
    {
        var subscriptionId = request.SubscriptionId;
        var subscription = await _subscriptionRespository.GetSubscriptionByIdAsync(subscriptionId);

        if (subscription is null)
        {
            return SubscriptionErrors.SubscriptionNotFound;
        }

        return subscription;
    }
}

