using Ardalis.SmartEnum;
using DomeGym.Domain.Common.Constants;

namespace DomeGym.Domain.SubscriptionAggregate;

public class SubscriptionType : SmartEnum<SubscriptionType>
{
    public static SubscriptionType FreeSubscription =>
        new SubscriptionType(DomainConstants.FreeSubscription.SubscriptionName, 0);

    public static SubscriptionType PremiumSubscription =>
        new SubscriptionType(DomainConstants.PremiumSubscription.SubscriptionName, 1);

    public SubscriptionType(string subscriptionName, int subscriptionValue)
        : base(subscriptionName, subscriptionValue)
    {
        
    }
}