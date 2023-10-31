using DomeGym.Domain.Constants;
using DomeGym.Domain.ErrorCodes;
using DomeGym.Domain.Interfaces;
using ErrorOr;

namespace DomeGym.Domain;

public class Gym : IHasId
{
    private readonly List<Guid> _roomIds = new List<Guid>();

    private readonly int _maxNumberOfRooms;

    public Guid Id { get; }

    public Gym(
        int maxNumberOfRooms,
        Guid id)
    {
        _maxNumberOfRooms = maxNumberOfRooms;
        Id = id;
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
