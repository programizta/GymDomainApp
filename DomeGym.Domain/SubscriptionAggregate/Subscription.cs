using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Constants;
using DomeGym.Domain.GymAggregate;
using ErrorOr;

namespace DomeGym.Domain.SubscriptionAggregate;
public class Subscription : AggregateRoot
{
    public List<Guid> GymIds { get; }

    public SubscriptionDetails SubscriptionDetails { get; private set; }

    public Subscription()
     : base(Guid.NewGuid())
    {
        
    }

    public Subscription(SubscriptionDetails subscriptionDetails, Guid? subscriptionId = null)
        : base(subscriptionId ?? Guid.NewGuid())
    {
        GymIds = new();
        SubscriptionDetails = subscriptionDetails;
    }

    public ErrorOr<Success> AssignGymToSubscription(Gym gym)
    {
        // if the subscription is premium or the number of maximum allowed gyms in the subscription
        // hasn't been exceeded, we should allow adding the gym to the subscription
        if (SubscriptionDetails.MaxNumberOfGymsAllowed != DomainConstants.SYSTEM_VALUE
            && GymIds.Count >= SubscriptionDetails.MaxNumberOfGymsAllowed)
        {
            return SubscriptionErrors.CannotAssignMoreGymsToTheSubscription;
        }

        GymIds.Add(gym.Id);

        return Result.Success;
    }
}
