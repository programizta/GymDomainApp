namespace DomeGym.Domain;

public class SubscriptionDetails
{
    public string SubscriptionName { get; }

    public int MaxNumberOfGyms { get; }

    public int MaxNumberOfRoomsInGym { get; }

    public int MaxNumberOfSessionsInRoom { get; }

    public SubscriptionDetails(
        string subscriptionName,
        int maxNumberOfGyms,
        int maxNumberOfRoomsInGym,
        int maxNumberOfSessionsInRoom)
    {
        SubscriptionName = subscriptionName;
        MaxNumberOfGyms = maxNumberOfGyms;
        MaxNumberOfRoomsInGym = maxNumberOfRoomsInGym;
        MaxNumberOfSessionsInRoom = maxNumberOfSessionsInRoom;
    }
}