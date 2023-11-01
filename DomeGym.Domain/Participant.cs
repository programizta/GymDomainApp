using DomeGym.Domain.ErrorCodes;
using ErrorOr;

namespace DomeGym.Domain;

public class Participant
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new List<Guid>();
    private readonly Schedule _schedule;

    public Guid ParticipantId { get; }

    public Participant(Guid userId,
        Guid? participantId = null)
    {
        _userId = userId;
        ParticipantId = participantId ?? Guid.NewGuid();
        _schedule = new Schedule(id: Guid.NewGuid());
    }

    public ErrorOr<Success> AssignSession(Session session)
    {
        if (session.StartTime >= session.EndTime)
        {
            return GeneralErrors.TimeInvalid;
        }

        if (IsOverlappingSession(session))
        {
            return ParticipantErrors.CannotAssignToOverlappingSession;
        }

        _schedule.BookSession(
            date: session.Date,
            startTime: session.StartTime,
            endTime: session.EndTime);
        _sessionIds.Add(session.Id);

        return Result.Success;
    }

    private bool IsOverlappingSession(Session session)
    {
        // if for a specified day there are no sessions already scheduled, return false immediately
        if (!_schedule.TimeSlotsForDays.TryGetValue(session.Date, out var timeSlots))
        {
            return false;
        }

        var potentialTimeSlotSessionToBook = new TimeSlot(
            session.StartTime,
            session.EndTime);

        return timeSlots.Any(x => !x.CanBookTimeSlot(potentialTimeSlotSessionToBook));
    }
}
