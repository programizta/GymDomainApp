using DomeGym.Domain.Constants;
using DomeGym.Domain.UnitTests.TestUtils.Sessions;
using Xunit;

namespace DomeGym.Domain.UnitTests;

public class TrainerTests
{
    [Fact]
    public void AssigningOverlappingSessionToTrainer_AssignShouldFail()
    {
        // Arrange
        // create a trainer
        // create two overlapping sessions
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
        var assigningSession2ToTrainerResult = trainer.AssignSession(session1);

        // Assert
        // check the result of assigning first session to trainer - should succeed
        // check the result of assigning second session to trainer - should fail
        // error should be of type TrainerErrors.CannotAssignOverlappingSessionsToTrainer
        assigningSession1ToTrainerResult.IsError.Should().BeFalse();
        assigningSession2ToTrainerResult.IsError.Should().BeTrue();
        assigningSession2ToTrainerResult.FirstError.Should().Be(TrainerErrors.CannotAssignOverlappingSessionsToTrainer);
    }
}