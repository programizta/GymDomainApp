using System;

namespace DomeGym.Domain.UnitTests.TestUtils.Sessions;

public static class ParticipantFactory
{
    public static Participant CreateParticipant(Guid? userId = null,
        Guid? participantId = null)
    {
        return new Participant(userId: userId ?? TestConstants.User.Id,
            participantId: participantId ?? TestConstants.Participant.Id);
    }
}