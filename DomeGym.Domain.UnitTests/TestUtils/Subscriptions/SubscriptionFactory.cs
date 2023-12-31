using System;
using DomeGym.Domain.SubscriptionAggregate;

namespace DomeGym.Domain.UnitTests.TestUtils.Subscriptions;

public static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        SubscriptionDetails subscriptionDetails,
        Guid? id = null)
    {
        return new Subscription(
            subscriptionDetails: subscriptionDetails,
            adminId: id ?? TestConstants.Admin.Id,
            subscriptionId: id ?? TestConstants.Subscription.Id);
    }
}