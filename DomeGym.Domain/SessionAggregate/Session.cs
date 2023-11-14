using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Interfaces;
using DomeGym.Domain.ParticipantAggregate;
using ErrorOr;

namespace DomeGym.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    public List<Reservation> Reservations { get; }

    public int MaximumNumberOfParticipants { get; private set; }

    public DateOnly Date { get; private set; }

    public TimeOnly StartTime { get; }

    public TimeOnly EndTime { get; private set; }

    public Session()
     : base(Guid.NewGuid())
    {
        
    }

    public Session(DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        int maximumNumberOfParticipants,
        Guid? sessionId = null)
            : base(sessionId ?? Guid.NewGuid())
    {
        Reservations = new();
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        MaximumNumberOfParticipants = maximumNumberOfParticipants;
    }

    public ErrorOr<Success> CancelReservation(Participant participant,
        IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
        {
            return SessionErrors.CannotCancelReservationInLessThan24HoursBeforeSessionStarts;
        }

        var reservation = Reservations.FirstOrDefault(x => x.ParticipantId == participant.Id);

        if (reservation is null)
        {
            return SessionErrors.ReservationDoesNotExist;
        }

        Reservations.Remove(reservation);

        return Result.Success;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (Reservations.Count >= MaximumNumberOfParticipants)
        {
            return SessionErrors.NoMoreFreeSlotsForReservation;
        }

        var newReservation = new Reservation(participant.Id);
        Reservations.Add(newReservation);

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