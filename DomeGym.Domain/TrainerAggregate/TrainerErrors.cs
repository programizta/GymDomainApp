using ErrorOr;

namespace DomeGym.Domain.TrainerAggregate;

public static class TrainerErrors
{
    public static Error CannotAssignOverlappingSessionToTrainer =>
        Error.Validation(code: "TrainerErrors.CannotAssignOverlappingSessionToTrainer",
        description: "Cannot assign more an overlapping session to trainer");
}