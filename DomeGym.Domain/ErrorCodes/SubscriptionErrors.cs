using ErrorOr;

namespace DomeGym.Domain.ErrorCodes;

public static class SubscriptionErrors
{
    public static Error CannotAssignMoreGymsToTheSubscription =>
        Error.Validation(code: "SubscriptionErrors.CannotAssignMoreGymsToTheSubscription",
        description: "Cannot assign more gyms to the subscription than the subscription allows");
}