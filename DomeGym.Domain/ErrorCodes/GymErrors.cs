using ErrorOr;

namespace DomeGym.Domain.ErrorCodes;

public static class GymErrors
{
    public static Error CannotAssignMoreRoomsThanSubscriptionAllows =>
        Error.Validation(code: "Gym.CannotAssignMoreRoomsThanSubscriptionAllows",
        description: "Gym cannot have more rooms than the current subscription allows");
}