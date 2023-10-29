using ErrorOr;

namespace DomeGym.Domain.ErrorCodes;

public static class RoomErrors
{
    public static Error CannotAssignMoreSessionsToRoomWithCurrentSubscription =>
        Error.Validation(code: "Room.CannotAssignMoreSessionsToRoomWithCurrentSubscription",
        description: "Cannot assign any more sessions to the room with current subscription");
}