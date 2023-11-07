using ErrorOr;

namespace DomeGym.Domain.ParticipantAggregate;

public static class ParticipantErrors
{
    public static Error CannotAssignToOverlappingSession =>
        Error.Validation(code: "ParticipantErrors.CannotAssignToOverlappingSession",
        description: "Cannot assign a participant to overlapping session");
}