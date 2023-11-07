using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.Common.ErrorCodes;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new List<Guid>();
    private readonly Schedule _schedule;

    public Participant(Guid userId, Guid? participantId = null)
        : base(participantId ?? Guid.NewGuid())
    {
        _userId = userId;
        _schedule = new Schedule(scheduleId: Guid.NewGuid());
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
