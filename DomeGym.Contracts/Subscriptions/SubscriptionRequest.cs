namespace DomeGym.Contracts.Subscriptions;

public record SubscriptionRequest(Guid AdminId, SubscriptionType SubscriptionType);