using ErrorOr;

namespace DomeGym.Domain.SubscriptionAggregate;

public static class SubscriptionErrors
{
    public static Error CannotAssignMoreGymsToTheSubscription =>
        Error.Validation(code: "SubscriptionErrors.CannotAssignMoreGymsToTheSubscription",
        description: "Cannot assign more gyms to the subscription than the subscription allows");

    public static Error SubscriptionNotFound =>
        Error.NotFound(code: "SubscriptionErrors.SubscriptionNotFound",
        description: "Subscription could not be found");
}