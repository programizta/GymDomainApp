using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Interfaces;
using DomeGym.Domain.ParticipantAggregate;
using ErrorOr;

namespace DomeGym.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private readonly Guid _trainerId;
    private readonly List<Reservation> _reservations = new();
    private readonly int _maximumNumberOfParticipants;

    public DateOnly Date { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public Session(DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        int maximumNumberOfParticipants,
        Guid trainerId,
        Guid? sessionId = null)
            : base(sessionId ?? Guid.NewGuid())
    {
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        _maximumNumberOfParticipants = maximumNumberOfParticipants;
        _trainerId = trainerId;
    }

    public ErrorOr<Success> CancelReservation(Participant participant,
        IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
        {
            return SessionErrors.CannotCancelReservationInLessThan24HoursBeforeSessionStarts;
        }

        var reservation = _reservations.FirstOrDefault(x => x.ParticipantId == participant.Id);

        if (reservation is null)
        {
            return SessionErrors.ReservationDoesNotExist;
        }

        _reservations.Remove(reservation);

        return Result.Success;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_reservations.Count >= _maximumNumberOfParticipants)
        {
            return SessionErrors.NoMoreFreeSlotsForReservation;
        }

        var newReservation = new Reservation(participant.Id);
        _reservations.Add(newReservation);

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime specifiedDateTime)
    {
        const int minimumNumberOfHours = 24;
        var sessionDateTime = new DateTime(Date.Year,
            Date.Month,
            Date.Day,
            StartTime.Hour,
            StartTime.Minute,
            StartTime.Second);

        return (sessionDateTime - specifiedDateTime).TotalHours < minimumNumberOfHours;
    }
}