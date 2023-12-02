using ErrorOr;

namespace DomeGym.Infrastructure.Subscription.Persistence;

public static class SubscriptionRepositoryErrors
{
    public static Error SubscriptionCurrentlyExists =>
        Error.Conflict(
            code: "SubscriptionRepositoryErrors.SubscriptionCurrentlyExists",
            description: "Cannot add subscription with the currently existing ID");
}