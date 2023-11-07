using System;
using DomeGym.Domain.RoomAggregate;
using DomeGym.Domain.SubscriptionAggregate;

namespace DomeGym.Domain.UnitTests.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        Subscription currentSubscription,
        Guid? id = null)
    {
        return new Room(maxNumberOfSessions: currentSubscription.SubscriptionDetails.MaxNumberOfSessionsInRoom,
            roomId: id ?? TestConstants.Room.Id);
    }
}