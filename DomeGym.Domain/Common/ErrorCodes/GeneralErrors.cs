using ErrorOr;

namespace DomeGym.Domain.Common.ErrorCodes;

public static class GeneralErrors
{
    public static Error TimeRangeInvalid =>
        Error.Validation("General.TimeRangeInvalid", "Time range is invalid");

    public static Error DateInvalid =>
        Error.Validation("General.DateInvalid", "Start and end date must be on the same day");
}