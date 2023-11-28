using DomeGym.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Subscription.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler
    : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
{
    private readonly ISubscriptionRespository _subscriptionRespository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubscriptionCommandHandler(
        ISubscriptionRespository subscriptionRespository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionRespository = subscriptionRespository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscriptionId = request.SubscriptionId;
        var isDeletingSubscriptionSuccessful = await _subscriptionRespository.DeleteSubscriptionByIdAsync(subscriptionId);

        if (!isDeletingSubscriptionSuccessful)
        {
            return SubscriptionPersistenceErrorCodes.SubscriptionNotFound;
        }

        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
