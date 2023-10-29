using System;

namespace DomeGym.Domain.UnitTests.TestUtils.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        int maxNumberOfRooms,
        Guid? id = null)
    {
        return new Gym(id: id ?? TestConstants.Gym.Id, maxNumberOfRooms: maxNumberOfRooms);
    }
}