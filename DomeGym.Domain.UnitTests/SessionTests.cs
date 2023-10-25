using System;
using DomeGym.Domain.UnitTests.TestUtils.Participants;
using DomeGym.Domain.UnitTests.TestUtils.Services;
using DomeGym.Domain.UnitTests.TestUtils.Sessions;
using FluentAssertions;
using Xunit;

namespace DomeGym.Domain.UnitTests;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(maximumNumberOfParticipants: 1);
        var participant1 = ParticipantFactory.CreateParticipant(userId: Guid.NewGuid(),
            participantId: Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(userId: Guid.NewGuid(),
            participantId: Guid.NewGuid());

        // Act
        var validReservationAction = () => session.ReserveSpot(participant1);
        var invalidReservationAction = () => session.ReserveSpot(participant2);

        // Assert
        validReservationAction.Should().NotThrow();
        invalidReservationAction.Should().ThrowExactly<Exception>();
    }

    // in current development, the subscription hasn't been impemented yet!
    [Fact]
    public void CancelReservationLessThan24HoursBeforeSession_FreeSubscription_ShouldFailCancellation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime,
            endTime: TestConstants.Session.EndTime,
            maximumNumberOfParticipants: 1);

        var participant = ParticipantFactory.CreateParticipant();

        session.ReserveSpot(participant);

        // if we specify the same DateTime as the starting date of a session
        // we should expect that cancellation of the reservation should fail
        var cancellationDateTime = TestConstants.Session.Date.ToDateTime(TimeOnly.MinValue);

        // Act
        var cancellationAction = () => session.CancelReservation(
            participant,
            new TestDateTimeProvider(cancellationDateTime));

        cancellationAction.Should().ThrowExactly<Exception>();
    }
}