using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Constants;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    public List<Guid> SessionIds { get; }

    public Schedule Schedule { get; private set; }

    public int MaxNumberOfSessions { get; private set; }

    public Room()
     : base(Guid.NewGuid())
    {

    }

    public Room(int maxNumberOfSessions, Guid? roomId = null)
        : base(roomId ?? Guid.NewGuid())
    {
        MaxNumberOfSessions = maxNumberOfSessions;
        Schedule = new Schedule(scheduleId: Guid.NewGuid());
        SessionIds = new List<Guid>(MaxNumberOfSessions);
    }

    public ErrorOr<Success> AddSession(Session session)
    {
        // if user has got premium subscription or number of sessions in room doesn't exceed the number
        // constrained by subscription, he should be able to add the session to the room
        if (MaxNumberOfSessions != DomainConstants.SYSTEM_VALUE
            && MaxNumberOfSessions <= SessionIds.Count)
        {
            return RoomErrors.CannotAssignMoreSessionsToRoomWithCurrentSubscription;
        }

        if (IsSessionOverlapping(session))
        {
            return RoomErrors.CannotAssignToRoomOverlappingSession;
        }

        Schedule.BookSession(
            date: session.Date,
            startTime: session.StartTime,
            endTime: session.EndTime);
        SessionIds.Add(session.Id);

        return Result.Success;
    }

    private bool IsSessionOverlapping(Session session)
    {
        // if there are no sessions booked for this date, return true immediately
        if (!Schedule.TimeSlotsForDays.TryGetValue(session.Date, out var timeSlots))
        {
            return false;
        }

        var potentialNewTimeSlotEntry = new TimeSlot(session.StartTime, session.EndTime); 

        return timeSlots.Any(x => !x.CanBookTimeSlot(potentialNewTimeSlotEntry));
    }

    // create a method (which will extend the AddSession), which will check (for the same date of added session)
    // if endTime of newly added session is earlier than startTime of currently checked session
    // or startTime of newly added session is later than endTime of currently checked session
    // if both requirements are met - add session to the room
}
