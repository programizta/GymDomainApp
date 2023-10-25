using DomeGym.Domain.Interfaces;
using ErrorOr;

namespace DomeGym.Domain;
public class Session
{
    private readonly Guid _sessionId;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new List<Guid>();
    private readonly DateOnly _date;
    private readonly TimeOnly _startTime;
    private readonly TimeOnly _endTime;
    private readonly int _maximumNumberOfParticipants;

    public Session(DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        int maximumNumberOfParticipants,
        Guid trainerId,
        Guid? sessionId = null)
    {
        _date = date;
        _startTime = startTime;
        _endTime = endTime;
        _maximumNumberOfParticipants = maximumNumberOfParticipants;
        _trainerId = trainerId;
        _sessionId = sessionId ?? Guid.NewGuid();
    }

    public void CancelReservation(Participant participant,
        IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
        {
            throw new Exception("Cannot cancel reservation in less than 24 hours before session starts with current subscription");
        }

        if (!_participantIds.Remove(participant.ParticipantId))
        {
            throw new Exception("Reservation does not exist");
        }
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maximumNumberOfParticipants)
        {
            return Error.Validation(description: "You cannot add more participants than available reservations in a session");
        }

        _participantIds.Add(participant.ParticipantId);

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime specifiedDateTime)
    {
        const int minimumNumberOfHours = 24;
        var sessionDateTime = new DateTime(_date.Year,
            _date.Month,
            _date.Day,
            _startTime.Hour,
            _startTime.Minute,
            _startTime.Second);

        return (sessionDateTime - specifiedDateTime).TotalHours < minimumNumberOfHours;
    }
}