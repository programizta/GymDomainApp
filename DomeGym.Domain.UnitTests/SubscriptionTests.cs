using DomeGym.Domain.Constants;
using DomeGym.Domain.ErrorCodes;
using DomeGym.Domain.UnitTests.TestUtils.Gyms;
using DomeGym.Domain.UnitTests.TestUtils.Subscriptions;
using FluentAssertions;
using Xunit;

namespace DomeGym.Domain.UnitTests;

public class SubscriptionTests
{
    [Fact]
    public void SubscriptionWithMoreGymsThanFreeSubscriptionAllows_AddingMoreShouldFail()
    {
        // Arrange
        // create free subscription
        // create two gyms
        var freeSubscription = SubscriptionFactory.CreateSubscription(DomainConstants.FreeSubscription);
        var gym1 = GymFactory.CreateGym(freeSubscription.SubscriptionDetails.MaxNumberOfRoomsInGym);
        var gym2 = GymFactory.CreateGym(freeSubscription.SubscriptionDetails.MaxNumberOfRoomsInGym);

        // Act
        // add two gyms to the subscription
        var assignGym1ToSubscriptionResult = freeSubscription.AssignGymToSubscription(gym1);
        var assignGym2ToSubscriptionResult = freeSubscription.AssignGymToSubscription(gym2);

        // Assert
        // check the result of adding first gym to the subscription - should be successful
        // check the result of adding second gym to the subscription - should fail and should
        // be of type SubscriptionErrors.CannotAssignMoreGymsToTheSubscription;
        assignGym1ToSubscriptionResult.IsError.Should().BeFalse();
        assignGym2ToSubscriptionResult.IsError.Should().BeTrue();
        assignGym2ToSubscriptionResult.FirstError.Should().Be(SubscriptionErrors.CannotAssignMoreGymsToTheSubscription);
    }
}