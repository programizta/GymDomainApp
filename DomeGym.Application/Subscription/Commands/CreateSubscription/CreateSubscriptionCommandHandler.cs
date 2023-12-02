using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using ErrorOr;
using MediatR;
using DomeGym.Application.Common.Interfaces;

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
        var subscriptionDetails = await _subscriptionRespository.GetSubscriptionDetailsByName(request.SubscriptionType);

        if (subscriptionDetails is null)
        {
            return Error.NotFound(description: "Details for subscription could not be found");
        }

        var subscriptionToSave = new SubscriptionEntity(subscriptionDetails, request.AdminId);

        // TODO: inside "CreateSubscriptionAsync" implement validation on DomainKey
        var addingSubscriptionResult = await _subscriptionRespository.AddSubscriptionAsync(subscriptionToSave);

        if (addingSubscriptionResult.IsError)
        {
            return addingSubscriptionResult.Errors;
        }

        await _unitOfWork.CommitChangesAsync();

        return subscriptionToSave;
    }
}