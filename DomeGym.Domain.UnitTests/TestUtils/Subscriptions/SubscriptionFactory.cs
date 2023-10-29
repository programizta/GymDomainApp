using System;
using DomeGym.Domain.Constants;

namespace DomeGym.Domain.UnitTests.TestUtils.Subscriptions;

public static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        SubscriptionDetails subscriptionDetails,
        Guid? id = null)
    {
        return new Subscription(
            subscriptionDetails: subscriptionDetails,
            id: id ?? TestConstants.Subscription.Id);
    }
}