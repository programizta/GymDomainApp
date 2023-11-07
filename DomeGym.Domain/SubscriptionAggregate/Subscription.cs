using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Constants;
using DomeGym.Domain.GymAggregate;
using ErrorOr;

namespace DomeGym.Domain.SubscriptionAggregate;
public class Subscription : AggregateRoot
{
    private readonly List<Guid> _gymIds = new List<Guid>();

    public SubscriptionDetails SubscriptionDetails { get; }

    public Subscription(SubscriptionDetails subscriptionDetails, Guid? subscriptionId = null)
        : base(subscriptionId ?? Guid.NewGuid())
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
