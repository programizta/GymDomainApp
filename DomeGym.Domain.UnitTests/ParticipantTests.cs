using System;
using DomeGym.Domain.ErrorCodes;
using DomeGym.Domain.UnitTests.TestUtils.Participants;
using DomeGym.Domain.UnitTests.TestUtils.Sessions;
using FluentAssertions;
using Xunit;

namespace DomeGym.Domain.UnitTests;

public class ParticipantTests
{
    [Fact]
    public void ReservingOverlappingSession_ReservationShouldFail()
    {
        // Arrange
        var participant = ParticipantFactory.CreateParticipant(
            userId: Guid.NewGuid(),
            participantId: Guid.NewGuid());
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

        // Act
        var assignSession1ToParticipantResult = participant.AssignSession(session1);
        var assignSession2ToParticipantResult = participant.AssignSession(session2);

        // Assert
        assignSession1ToParticipantResult.IsError.Should().BeFalse();
        assignSession2ToParticipantResult.IsError.Should().BeTrue();
        assignSession2ToParticipantResult.FirstError.Should().Be(ParticipantErrors.CannotAssignToOverlappingSession);
    }
}