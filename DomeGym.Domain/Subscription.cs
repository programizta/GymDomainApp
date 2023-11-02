using DomeGym.Domain.Common;
using DomeGym.Domain.Constants;
using DomeGym.Domain.ErrorCodes;
using DomeGym.Domain.Interfaces;
using ErrorOr;

namespace DomeGym.Domain;
public class Subscription : EntityBase
{
    private readonly List<Guid> _gymIds = new List<Guid>();

    public SubscriptionDetails SubscriptionDetails { get; }

    public Subscription(
        SubscriptionDetails subscriptionDetails,
        Guid? id = null)
            : base(id ?? Guid.NewGuid())
    {
        SubscriptionDetails = subscriptionDetails;
    }

    public ErrorOr<Success> AssignGymToSubscription(Gym gym)
    {
        // if the subscription is premium or the number of maximum allowed gyms in the subscription
        // hasn't been exceeded, we should allow adding the gym to the subscription
        if (SubscriptionDetails.MaxNumberOfGymsAllowed != DomainConstants.SYSTEM_VALUE
            && _gymIds.Count >= SubscriptionDetails.MaxNumberOfGymsAllowed)
        {
            return SubscriptionErrors.CannotAssignMoreGymsToTheSubscription;
        }

        _gymIds.Add(gym.Id);

        return Result.Success;
    }
}
