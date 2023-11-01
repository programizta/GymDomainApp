using ErrorOr;

namespace DomeGym.Domain.ErrorCodes;

public static class TrainerErrors
{
    public static Error CannotAssignOverlappingSessionToTrainer =>
        Error.Validation(code: "TrainerErrors.CannotAssignOverlappingSessionToTrainer",
        description: "Cannot assign more an overlapping session to trainer");
}