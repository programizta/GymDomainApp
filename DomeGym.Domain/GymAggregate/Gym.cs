using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Constants;
using DomeGym.Domain.Common.Interfaces;
using DomeGym.Domain.RoomAggregate;
using ErrorOr;

namespace DomeGym.Domain.GymAggregate;

public class Gym : AggregateRoot, IHasId
{
    private readonly List<Guid> _roomIds = new List<Guid>();

    private readonly int _maxNumberOfRooms;

    public Gym(int maxNumberOfRooms, Guid? gymId = null)
        : base(gymId ?? Guid.NewGuid())
    {
        _maxNumberOfRooms = maxNumberOfRooms;
    }

    public ErrorOr<Success> AssignRoom(Room room)
    {
        // if user has got premium subscription or number of rooms in gym doesn't exceed the number
        // constrained by subscription, he should be able to add the room to the gym
        if (_maxNumberOfRooms != DomainConstants.SYSTEM_VALUE
            && _roomIds.Count >= _maxNumberOfRooms)
        {
            return GymErrors.CannotAssignMoreRoomsThanSubscriptionAllows;
        }

        _roomIds.Add(room.Id);

        return Result.Success;
    }
}
