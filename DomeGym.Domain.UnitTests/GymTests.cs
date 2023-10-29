using System;
using DomeGym.Domain.Constants;
using DomeGym.Domain.ErrorCodes;
using DomeGym.Domain.UnitTests.TestUtils.Gyms;
using DomeGym.Domain.UnitTests.TestUtils.Rooms;
using DomeGym.Domain.UnitTests.TestUtils.Subscriptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Xunit;

namespace DomeGym.Domain.UnitTests;

public class GymTests
{
    [Fact]
    public void GymWithMoreRoomsThanFreeSubscriptionAllows_CreationShouldFail()
    {
        // Arrange
        // create two rooms
        // create create a free subscription which allows only one room in the gym
        // create a gym, which will only allow having max number of rooms, satisfying the subscription
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
        // assign to gym two rooms
        var assignRoom1ToGymResult = gym.AssignRoom(room1);
        var assignRoom2ToGymResult = gym.AssignRoom(room2);

        // Assert
        // check the result of assigning two rooms to gym
        // assigning first room to the gym should be successful
        assignRoom1ToGymResult.IsError.Should().BeFalse();

        // assigning second room should fail
        assignRoom2ToGymResult.IsError.Should().BeTrue();
        assignRoom2ToGymResult.FirstError.Should().Be(GymErrors.CannotAssignMoreRoomsThanSubscriptionAllows);
    }
}