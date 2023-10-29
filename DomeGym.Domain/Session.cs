using DomeGym.Domain.ErrorCodes;
using DomeGym.Domain.Interfaces;
using ErrorOr;

namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new List<Guid>();
    private readonly DateOnly _date;
    private readonly TimeOnly _startTime;
    private readonly TimeOnly _endTime;
    private readonly int _maximumNumberOfParticipants;

    public Guid Id { get; }

    public Session(DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        int maximumNumberOfParticipants,
        Guid trainerId,
        Guid? id = null)
    {
        _date = date;
        _startTime = startTime;
        _endTime = endTime;
        _maximumNumberOfParticipants = maximumNumberOfParticipants;
        _trainerId = trainerId;
        Id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> CancelReservation(Participant participant,
        IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
        {
            return SessionErrors.CannotCancelReservationInLessThan24HoursBeforeSessionStarts;
        }

        if (!_participantIds.Remove(participant.ParticipantId))
        {
            return SessionErrors.ReservationDoesNotExist;
        }

        return Result.Success;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maximumNumberOfParticipants)
        {
            return SessionErrors.NoMoreFreeSlotsForReservation;
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