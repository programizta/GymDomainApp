using DomeGym.Domain.Constants;
using DomeGym.Domain.ErrorCodes;
using ErrorOr;

namespace DomeGym.Domain;

public class Room
{
    public Guid Id { get; }

    private readonly List<Guid> _sessionIds = new List<Guid>();

    private readonly int _maxNumberOfSessions;

    public Room(Guid id, int maxNumberOfSessions)
    {
        Id = id;
        _maxNumberOfSessions = maxNumberOfSessions;
    }

    public ErrorOr<Success> AddSession(Session session)
    {
        // if user has got premium subscription or number of sessions in room doesn't exceed the number
        // constrained by subscription, he should be able to add the session to the room
        if (_maxNumberOfSessions != DomainConstants.SYSTEM_VALUE
            || _maxNumberOfSessions <= _sessionIds.Count)
        {
            return RoomErrors.CannotAssignMoreSessionsToRoomWithCurrentSubscription;
        }

        _sessionIds.Add(session.Id);

        return Result.Success;
    }

    // create a method (which will extend the AddSession), which will check (for the same date of added session)
    // if endTime of newly added session is earlier than startTime of currently checked session
    // or startTime of newly added session is later than endTime of currently checked session
    // if both requirements are met - add session to the room
}
