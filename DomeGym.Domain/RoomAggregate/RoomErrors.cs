using ErrorOr;

namespace DomeGym.Domain.RoomAggregate;

public static class RoomErrors
{
    public static Error CannotAssignMoreSessionsToRoomWithCurrentSubscription =>
        Error.Validation(code: "Room.CannotAssignMoreSessionsToRoomWithCurrentSubscription",
        description: "Cannot assign any more sessions to the room with current subscription");

    public static Error CannotAssignToRoomOverlappingSession =>
        Error.Validation(code: "Room.CannotAssignToRoomOverlappingSession",
        description: "Cannot assign overlapping sessions to the room");
}