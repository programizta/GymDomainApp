using DomeGym.Domain.Constants;

namespace DomeGym.Domain;
public class Subscription
{
    private readonly Guid _id;

    private readonly List<Guid> _gymIds = new List<Guid>();

    public SubscriptionDetails SubscriptionDetails { get; }

    public Subscription(
        SubscriptionDetails subscriptionDetails,
        Guid? id = null)
    {
        SubscriptionDetails = subscriptionDetails;
        _id = id ?? Guid.NewGuid();
    }
}
