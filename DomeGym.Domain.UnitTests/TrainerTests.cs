using DomeGym.Domain.Constants;
using DomeGym.Domain.ErrorCodes;
using DomeGym.Domain.UnitTests.TestUtils.Sessions;
using DomeGym.Domain.UnitTests.TestUtils.Trainers;
using FluentAssertions;
using Xunit;

namespace DomeGym.Domain.UnitTests;

public class TrainerTests
{
    [Fact]
    public void AssigningOverlappingSessionToTrainer_AssignShouldFail()
    {
        // Arrange
        var trainer = TrainerFactory.CreateTrainer();
        var session1 = SessionFactory.CreateSession(
            date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime1,
            endTime: TestConstants.Session.EndTime1);

        var session2 = SessionFactory.CreateSession(
            date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime1,
            endTime: TestConstants.Session.EndTime1);

        // Act
        var assigningSession1ToTrainerResult = trainer.AssignSession(session1);
        var assigningSession2ToTrainerResult = trainer.AssignSession(session2);

        // Assert
        assigningSession1ToTrainerResult.IsError.Should().BeFalse();
        assigningSession2ToTrainerResult.IsError.Should().BeTrue();
        assigningSession2ToTrainerResult.FirstError.Should().Be(TrainerErrors.CannotAssignOverlappingSessionToTrainer);
    }

    [Fact]
    public void AssigningNoOverlappingSessionsToTrainer_AssignShouldSucceed()
    {
        // Arrange
        var trainer = TrainerFactory.CreateTrainer();
        var session1 = SessionFactory.CreateSession(
            date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime1,
            endTime: TestConstants.Session.EndTime1);

        var session2 = SessionFactory.CreateSession(
            date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime2,
            endTime: TestConstants.Session.EndTime2);

        // Act
        var assigningSession1ToTrainerResult = trainer.AssignSession(session1);
        var assigningSession2ToTrainerResult = trainer.AssignSession(session2);

        // Assert
        assigningSession1ToTrainerResult.IsError.Should().BeFalse();
        assigningSession2ToTrainerResult.IsError.Should().BeFalse();
    }
}