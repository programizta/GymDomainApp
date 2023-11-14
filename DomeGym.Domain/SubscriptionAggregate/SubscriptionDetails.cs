using DomeGym.Domain.Common;

namespace DomeGym.Domain.SubscriptionAggregate;

public class SubscriptionDetails : EntityBase
{
    public string SubscriptionName { get; private set; } = null!;

    public int MaxNumberOfGyms { get; private set; }

    public int MaxNumberOfRoomsInGym { get; private set; }

    public int MaxNumberOfSessionsInRoom { get; private set; }

    public int MaxNumberOfGymsAllowed { get; private set; }

    public SubscriptionDetails()
        : base(Guid.NewGuid())
    {
        
    }

    public SubscriptionDetails(
        string subscriptionName,
        int maxNumberOfGyms,
        int maxNumberOfRoomsInGym,
        int maxNumberOfSessionsInRoom,
        int maxNumberOfGymsAllowed)
        : base(Guid.NewGuid())
    {
        SubscriptionName = subscriptionName;
        MaxNumberOfGyms = maxNumberOfGyms;
        MaxNumberOfRoomsInGym = maxNumberOfRoomsInGym;
        MaxNumberOfSessionsInRoom = maxNumberOfSessionsInRoom;
        MaxNumberOfGymsAllowed = maxNumberOfGymsAllowed;
    }
}