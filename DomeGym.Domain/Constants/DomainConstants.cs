namespace DomeGym.Domain.Constants;

/// <summary>
/// This class is meant to represent domain constants
/// TODO: implement a nicer (class or enum-wrapped) way of constant data representation
/// </summary>
public static class DomainConstants
{
    public static SubscriptionDetails FreeSubscription =>
        new SubscriptionDetails(
            subscriptionName: "Free subscription",
            maxNumberOfGyms: 1,
            maxNumberOfRoomsInGym: 1,
            maxNumberOfSessionsInRoom: 1,
            maxNumberOfGymsAllowed: 1);

    public static SubscriptionDetails PremiumSubscription =>
        new SubscriptionDetails(subscriptionName: "Premium subscription",
            maxNumberOfGyms: SYSTEM_VALUE,
            maxNumberOfRoomsInGym: SYSTEM_VALUE,
            maxNumberOfSessionsInRoom: SYSTEM_VALUE,
            maxNumberOfGymsAllowed: SYSTEM_VALUE);

    public const int SYSTEM_VALUE = -1;
}