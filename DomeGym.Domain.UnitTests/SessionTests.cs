using System;
using DomeGym.Domain.Constants;
using DomeGym.Domain.ErrorCodes;
using DomeGym.Domain.UnitTests.TestUtils.Participants;
using DomeGym.Domain.UnitTests.TestUtils.Services;
using DomeGym.Domain.UnitTests.TestUtils.Sessions;
using DomeGym.Domain.UnitTests.TestUtils.Subscriptions;
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
        var participant1ReservationResult = session.ReserveSpot(participant1);
        var participant2ReservationResult = session.ReserveSpot(participant2);

        // Assert
        participant1ReservationResult.IsError.Should().BeFalse();
        participant2ReservationResult.FirstError.Should().Be(SessionErrors.NoMoreFreeSlotsForReservation);
    }

    // TODO: in current development, the subscription hasn't been impemented yet!
    [Fact]
    public void CancelReservationLessThan24HoursBeforeSession_FreeSubscription_ShouldFailCancellation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime1,
            endTime: TestConstants.Session.EndTime1,
            maximumNumberOfParticipants: 1);
        var subscription = SubscriptionFactory.CreateSubscription(DomainConstants.FreeSubscription);
        var participant = ParticipantFactory.CreateParticipant();

        var reserveSpotResult = session.ReserveSpot(participant);

        // if we specify the same DateTime as the starting date of a session
        // we should expect that cancellation of the reservation should fail
        var cancellationDateTime = TestConstants.Session.Date.ToDateTime(TimeOnly.MinValue);

        // Act
        var cancellationResult = session.CancelReservation(
            participant,
            new TestDateTimeProvider(cancellationDateTime));

        reserveSpotResult.IsError.Should().BeFalse();
        cancellationResult.IsError.Should().BeTrue();
        cancellationResult.FirstError.Should().Be(SessionErrors.CannotCancelReservationInLessThan24HoursBeforeSessionStarts);
    }
}