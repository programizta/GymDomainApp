using System;
using DomeGym.Domain.Constants;
using DomeGym.Domain.ErrorCodes;
using DomeGym.Domain.UnitTests.TestUtils.Gyms;
using DomeGym.Domain.UnitTests.TestUtils.Rooms;
using DomeGym.Domain.UnitTests.TestUtils.Subscriptions;
using FluentAssertions;
using Xunit;

namespace DomeGym.Domain.UnitTests;

public class GymTests
{
    [Fact]
    public void GymWithMoreRoomsThanFreeSubscriptionAllows_CreationShouldFail()
    {
        // Arrange
        var freeSubscription = SubscriptionFactory.CreateSubscription(
            subscriptionDetails: DomainConstants.FreeSubscription,
            id: Guid.NewGuid());
        var room1 = RoomFactory.CreateRoom(
            currentSubscription: freeSubscription,
            id: Guid.NewGuid());
        var room2 = RoomFactory.CreateRoom(
            currentSubscription: freeSubscription,
            id: Guid.NewGuid());
        var gym = GymFactory.CreateGym(
            id: Guid.NewGuid(),
            maxNumberOfRooms: freeSubscription.SubscriptionDetails.MaxNumberOfRoomsInGym);

        // Act
        var assignRoom1ToGymResult = gym.AssignRoom(room1);
        var assignRoom2ToGymResult = gym.AssignRoom(room2);

        // Assert
        assignRoom1ToGymResult.IsError.Should().BeFalse();

        assignRoom2ToGymResult.IsError.Should().BeTrue();
        assignRoom2ToGymResult.FirstError.Should().Be(GymErrors.CannotAssignMoreRoomsThanSubscriptionAllows);
    }
}