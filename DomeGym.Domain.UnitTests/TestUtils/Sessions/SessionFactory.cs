using System;

namespace DomeGym.Domain.UnitTests.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        DateOnly? date = null,
        TimeOnly? startTime = null,
        TimeOnly? endTime = null,
        int maximumNumberOfParticipants = TestConstants.Session.MAX_NUMBER_OF_PARTICIPANTS,
        Guid? sessionId = null)
    {
        return new Session(
            date: date ?? TestConstants.Session.Date,
            startTime: startTime ?? TestConstants.Session.StartTime,
            endTime: endTime ?? TestConstants.Session.EndTime,
            maximumNumberOfParticipants,
            trainerId: TestConstants.Trainer.Id,
            sessionId: sessionId ?? TestConstants.Session.Id);
    }
}