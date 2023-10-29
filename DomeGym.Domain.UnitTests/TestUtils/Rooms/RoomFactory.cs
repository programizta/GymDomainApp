using System;

namespace DomeGym.Domain.UnitTests.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        Subscription currentSubscription,
        Guid? id = null)
    {
        return new Room(id: id ?? TestConstants.Room.Id,
            maxNumberOfSessions: currentSubscription.SubscriptionDetails.MaxNumberOfSessionsInRoom);
    }
}