using System;
using DomeGym.Domain.TrainerAggregate;
using DomeGym.Domain.UnitTests.TestUtils.Sessions;
using DomeGym.Domain.UnitTests.TestUtils.Trainers;
using FluentAssertions;
using Xunit;

namespace DomeGym.Domain.UnitTests.Tests;

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
            endTime: TestConstants.Session.EndTime1,
            id: Guid.NewGuid());

        var session2 = SessionFactory.CreateSession(
            date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime1,
            endTime: TestConstants.Session.EndTime1,
            id: Guid.NewGuid());

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
            endTime: TestConstants.Session.EndTime1,
            id: Guid.NewGuid());

        var session2 = SessionFactory.CreateSession(
            date: TestConstants.Session.Date,
            startTime: TestConstants.Session.StartTime2,
            endTime: TestConstants.Session.EndTime2,
            id: Guid.NewGuid());

        // Act
        var assigningSession1ToTrainerResult = trainer.AssignSession(session1);
        var assigningSession2ToTrainerResult = trainer.AssignSession(session2);

        // Assert
        assigningSession1ToTrainerResult.IsError.Should().BeFalse();
        assigningSession2ToTrainerResult.IsError.Should().BeFalse();
    }
}