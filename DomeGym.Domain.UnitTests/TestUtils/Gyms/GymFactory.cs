using System;
using DomeGym.Domain.GymAggregate;

namespace DomeGym.Domain.UnitTests.TestUtils.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        int maxNumberOfRooms,
        Guid? id = null)
    {
        return new Gym(maxNumberOfRooms: maxNumberOfRooms, gymId: id ?? TestConstants.Gym.Id);
    }
}