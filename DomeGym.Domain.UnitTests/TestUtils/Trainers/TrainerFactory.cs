using System;
using DomeGym.Domain.TrainerAggregate;

namespace DomeGym.Domain.UnitTests.TestUtils.Trainers;

public static class TrainerFactory
{
    public static Trainer CreateTrainer(Guid? userId = null)
    {
        return new Trainer(trainerId: TestConstants.Trainer.Id);
    }
}