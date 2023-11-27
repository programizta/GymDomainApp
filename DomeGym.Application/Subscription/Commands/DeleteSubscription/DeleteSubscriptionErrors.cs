using ErrorOr;

namespace DomeGym.Application.Subscription.Commands.DeleteSubscription;

public static class SubscriptionPersistenceErrorCodes
{
    public static Error SubscriptionNotFound =>
        Error.NotFound(code: "SubscriptionPersistenceErrorCodes.SubscriptionNotFound",
            description: "Subscription with ID: {0} does not exist");
}