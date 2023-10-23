using System;

namespace DomeGym.Domain.UnitTests.TestUtils.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(int maximumNumberOfParticipants,
        Guid? sessionId = null)
    {
        return new Session(maximumNumberOfParticipants,
            trainerId: TestConstants.Trainer.Id,
            sessionId ?? TestConstants.Session.Id);
    }
}