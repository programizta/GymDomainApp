using System;
using DomeGym.Domain.ParticipantAggregate;

namespace DomeGym.Domain.UnitTests.TestUtils.Participants;

public static class ParticipantFactory
{
    public static Participant CreateParticipant(Guid? participantId = null)
    {
        return new Participant(participantId: participantId ?? TestConstants.Participant.Id);
    }
}