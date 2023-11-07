using System;
using DomeGym.Domain.SessionAggregate;

namespace DomeGym.Domain.UnitTests.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        DateOnly? date = null,
        TimeOnly? startTime = null,
        TimeOnly? endTime = null,
        int maximumNumberOfParticipants = TestConstants.Session.MAX_NUMBER_OF_PARTICIPANTS,
        Guid? id = null)
    {
        return new Session(
            date: date ?? TestConstants.Session.Date,
            startTime: startTime ?? TestConstants.Session.StartTime1,
            endTime: endTime ?? TestConstants.Session.EndTime1,
            maximumNumberOfParticipants,
            trainerId: TestConstants.Trainer.Id,
            sessionId: id ?? TestConstants.Session.Id);
    }
}