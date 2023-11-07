namespace DomeGym.Domain.SubscriptionAggregate;

public class SubscriptionDetails
{
    public string SubscriptionName { get; }

    public int MaxNumberOfGyms { get; }

    public int MaxNumberOfRoomsInGym { get; }

    public int MaxNumberOfSessionsInRoom { get; }

    public int MaxNumberOfGymsAllowed { get; }

    public SubscriptionDetails(
        string subscriptionName,
        int maxNumberOfGyms,
        int maxNumberOfRoomsInGym,
        int maxNumberOfSessionsInRoom,
        int maxNumberOfGymsAllowed)
    {
        SubscriptionName = subscriptionName;
        MaxNumberOfGyms = maxNumberOfGyms;
        MaxNumberOfRoomsInGym = maxNumberOfRoomsInGym;
        MaxNumberOfSessionsInRoom = maxNumberOfSessionsInRoom;
        MaxNumberOfGymsAllowed = maxNumberOfGymsAllowed;
    }
}