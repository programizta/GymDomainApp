using System;
using DomeGym.Domain.TrainerAggregate;

namespace DomeGym.Domain.UnitTests.TestUtils.Trainers;

public static class TrainerFactory
{
    public static Trainer CreateTrainer(Guid? trainerId = null)
    {
        return new Trainer(trainerId: trainerId ?? TestConstants.Trainer.Id);
    }
}