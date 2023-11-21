using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using ErrorOr;
using MediatR;
using DomeGym.Domain.Common.Constants;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.SubscriptionAggregate;

namespace DomeGym.Application.Subscription.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler
    : IRequestHandler<CreateSubscriptionCommand, ErrorOr<SubscriptionEntity>>
{
    private readonly ISubscriptionRespository _subscriptionRespository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(ISubscriptionRespository subscriptionRespository, IUnitOfWork unitOfWork)
    {
        _subscriptionRespository = subscriptionRespository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<SubscriptionEntity>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        // TODO: temporarily check here if the request is having either 'Free' or 'Premium' subscription
        var subscriptionToSave = new SubscriptionEntity(request.SubscriptionType == SubscriptionType.FreeSubscription.Name ? DomainConstants.FreeSubscription : DomainConstants.PremiumSubscription, request.AdminId);

        // TODO: inside "CreateSubscriptionAsync" implement validation on DomainKey
        await _subscriptionRespository.AddSubscriptionAsync(subscriptionToSave);
        await _unitOfWork.CommitChangesAsync();

        return subscriptionToSave;
    }
}