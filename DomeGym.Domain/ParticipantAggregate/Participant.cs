using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    public List<Guid> SessionIds { get; }
    
    public Schedule Schedule { get; private set; }

    public Participant()
     : base(Guid.NewGuid())
    {
        Schedule = new Schedule(scheduleId: Guid.NewGuid());
    }

    public Participant(Guid? participantId = null)
        : base(participantId ?? Guid.NewGuid())
    {
        SessionIds = new List<Guid>();
        Schedule = new Schedule(scheduleId: Guid.NewGuid());
    }

    public ErrorOr<Success> AssignSession(Session session)
    {
        // TODO: implement nicer way of handing the exception while adding
        // existing session to the SessionIds
        if (SessionIds.Any(x => x == session.Id))
        {
            throw new Exception();
        }

        if (IsOverlappingSession(session))
        {
            return ParticipantErrors.CannotAssignToOverlappingSession;
        }

        Schedule.BookSession(
            date: session.Date,
            startTime: session.StartTime,
            endTime: session.EndTime);
        SessionIds.Add(session.Id);

        return Result.Success;
    }

    private bool IsOverlappingSession(Session session)
    {
        // if for a specified day there are no sessions already scheduled, return false immediately
        if (!Schedule.TimeSlotsForDays.TryGetValue(session.Date, out var timeSlots))
        {
            return false;
        }

        var potentialTimeSlotSessionToBook = new TimeSlot(
            session.StartTime,
            session.EndTime);

        return timeSlots.Any(x => !x.CanBookTimeSlot(potentialTimeSlotSessionToBook));
    }
}
