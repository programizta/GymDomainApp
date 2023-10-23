using System;
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
        var participant1 = ParticipantFactory.CreateParticipant(userId: Guid.NewGuid(), participantId: Guid.NewGuid());
        var participant2 = ParticipantFactory.CreateParticipant(userId: Guid.NewGuid(), participantId: Guid.NewGuid());

        // Act
        var validReservationAction = () => session.ReserveSpot(participant1);
        var invalidReservationAction = () => session.ReserveSpot(participant2);

        validReservationAction.Should().NotThrow();
        invalidReservationAction.Should().ThrowExactly<Exception>();
    }
}