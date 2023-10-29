namespace DomeGym.Domain;

public class Participant
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds;

    public Guid ParticipantId { get; }

    public Participant(Guid userId,
        Guid? participantId = null)
    {
        _userId = userId;
        ParticipantId = participantId ?? Guid.NewGuid();
    }
}
