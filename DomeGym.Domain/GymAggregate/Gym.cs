using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Constants;
using DomeGym.Domain.RoomAggregate;
using ErrorOr;

namespace DomeGym.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    public List<Guid> RoomIds { get; }

    public int MaxNumberOfRooms { get; private set; }

    public Gym()
     : base(Guid.NewGuid())
    {
        
    }

    public Gym(int maxNumberOfRooms, Guid? gymId = null)
        : base(gymId ?? Guid.NewGuid())
    {
        RoomIds = new List<Guid>();
        MaxNumberOfRooms = maxNumberOfRooms;
    }

    public ErrorOr<Success> AssignRoom(Room room)
    {
        // TODO: implement nicer way of handing the exception while adding
        // existing room to the RoomIds
        if (RoomIds.Any(x => x == room.Id))
        {
            throw new Exception();
        }

        // if user has got premium subscription or number of rooms in gym doesn't exceed the number
        // constrained by subscription, he should be able to add the room to the gym
        if (MaxNumberOfRooms != DomainConstants.SYSTEM_VALUE
            && RoomIds.Count >= MaxNumberOfRooms)
        {
            return GymErrors.CannotAssignMoreRoomsThanSubscriptionAllows;
        }

        RoomIds.Add(room.Id);

        return Result.Success;
    }
}
