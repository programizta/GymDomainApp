using System;

namespace DomeGym.Domain.UnitTests.TestUtils.Trainers;

public static class TrainerFactory
{
    public static Trainer CreateTrainer(Guid? userId = null)
    {
        return new Trainer(userId: userId ?? Guid.NewGuid())
    }
}