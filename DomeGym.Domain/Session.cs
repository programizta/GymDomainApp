namespace DomeGym.Domain;
public class Session
{
    private readonly Guid _sessionId;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new List<Guid>();
    private readonly int _maximumNumberOfParticipants;

    public Session(int maximumNumberOfParticipants,
        Guid trainerId,
        Guid? sessionId = null)
    {   
        _maximumNumberOfParticipants = maximumNumberOfParticipants;
        _trainerId = trainerId;
        _sessionId = sessionId ?? Guid.NewGuid();
    }

    public void ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maximumNumberOfParticipants)
        {
            throw new Exception("You cannot add more participants than available reservations in a session");
        }

        _participantIds.Add(participant.ParticipantId);
    }
}
