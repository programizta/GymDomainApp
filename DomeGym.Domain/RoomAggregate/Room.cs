using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Constants;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.Common.ErrorCodes;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private readonly List<Guid> _sessionIds = new List<Guid>();

    private readonly Schedule _schedule;

    private readonly int _maxNumberOfSessions;

    public Room(int maxNumberOfSessions, Guid? roomId = null)
        : base(roomId ?? Guid.NewGuid())
    {
        _maxNumberOfSessions = maxNumberOfSessions;
        _schedule = new Schedule(scheduleId: Guid.NewGuid());
    }

    public ErrorOr<Success> AddSession(Session session)
    {
        if (session.StartTime >= session.EndTime)
        {
            return GeneralErrors.TimeInvalid;
        }

        // if user has got premium subscription or number of sessions in room doesn't exceed the number
        // constrained by subscription, he should be able to add the session to the room
        if (_maxNumberOfSessions != DomainConstants.SYSTEM_VALUE
            && _maxNumberOfSessions <= _sessionIds.Count)
        {
            return RoomErrors.CannotAssignMoreSessionsToRoomWithCurrentSubscription;
        }

        if (IsSessionOverlapping(session))
        {
            return RoomErrors.CannotAssignToRoomOverlappingSession;
        }

        _schedule.BookSession(
            date: session.Date,
            startTime: session.StartTime,
            endTime: session.EndTime);
        _sessionIds.Add(session.Id);

        return Result.Success;
    }

    private bool IsSessionOverlapping(Session session)
    {
        // if there are no sessions booked for this date, return true immediately
        if (!_schedule.TimeSlotsForDays.TryGetValue(session.Date, out var timeSlots))
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
