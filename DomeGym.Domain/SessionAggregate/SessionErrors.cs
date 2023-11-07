using ErrorOr;

namespace DomeGym.Domain.SessionAggregate;

public static class SessionErrors
{
    public static Error NoMoreFreeSlotsForReservation =>
        Error.Validation(code: "Session.NoMoreFreeSlotsForReservation",
        description: "Cannot make a reservation for a participant due to no free room");

    public static Error CannotCancelReservationInLessThan24HoursBeforeSessionStarts =>
        Error.Validation(code: "Session.CannotCancelReservationInLessThan24HoursBeforeSessionStarts",
        description: "With current subscription you cannot cancel reservation in less than 24 hours before session starts");

    public static Error ReservationDoesNotExist =>
        Error.NotFound(code: "Session.ReservationDoesNotExist",
        description: "Reservation does not exist");
}