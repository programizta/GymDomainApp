using System;
using DomeGym.Domain.Constants;
using DomeGym.Domain.ErrorCodes;
using DomeGym.Domain.UnitTests.TestUtils.Rooms;
using DomeGym.Domain.UnitTests.TestUtils.Sessions;
using DomeGym.Domain.UnitTests.TestUtils.Subscriptions;
using FluentAssertions;
using Xunit;

namespace DomeGym.Domain.UnitTests;

public class RoomTests
{
    [Fact]
    public void RoomWithMoreSessionsThanSubscriptionAllows_AddingSessionShouldFail()
    {
        // Arrange
        var freeSubscription = SubscriptionFactory.CreateSubscription(DomainConstants.FreeSubscription);
        var room = RoomFactory.CreateRoom(id: Guid.NewGuid(),
            currentSubscription: freeSubscription);
        var session1 = SessionFactory.CreateSession(
            date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime1,
            endTime: TestConstants.Session.EndTime1,
            id: Guid.NewGuid());
        var session2 = SessionFactory.CreateSession(
            date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime2,
            endTime: TestConstants.Session.EndTime2,
            id: Guid.NewGuid());

        // Act
        var addingSession1ToRoomResult = room.AddSession(session1);
        var addingSession2ToRoomResult = room.AddSession(session2);

        // Assert
        addingSession1ToRoomResult.IsError.Should().BeFalse();
        addingSession2ToRoomResult.IsError.Should().BeTrue();
        addingSession2ToRoomResult.FirstError.Should().Be(RoomErrors.CannotAssignMoreSessionsToRoomWithCurrentSubscription);
    }

    [Fact]
    public void AddingOverlappingSessionToRoom_ShouldFail()
    {
        // Arrange
        var premiumSubsciption = SubscriptionFactory.CreateSubscription(DomainConstants.PremiumSubscription);

        // these two have overlapping session
        var session1 = SessionFactory.CreateSession(
            date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime1,
            endTime: TestConstants.Session.EndTime1,
            id: Guid.NewGuid());
        var session2 = SessionFactory.CreateSession(
            date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime1,
            endTime: TestConstants.Session.EndTime1,
            id: Guid.NewGuid());
        var room = RoomFactory.CreateRoom(id: Guid.NewGuid(),
            currentSubscription: premiumSubsciption);

        // Act
        var addingSession1ToRoomResult = room.AddSession(session1);
        var addingSession2ToRoomResult = room.AddSession(session2);

        // Assert
        // first session assignment to room should succeed
        // second session assignment to room should fail, with details of failure
        addingSession1ToRoomResult.IsError.Should().BeFalse();
        addingSession2ToRoomResult.IsError.Should().BeTrue();
        addingSession2ToRoomResult.FirstError.Should().Be(RoomErrors.CannotAssignToRoomOverlappingSession);

    }
}