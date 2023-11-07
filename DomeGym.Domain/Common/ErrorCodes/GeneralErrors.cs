using ErrorOr;

namespace DomeGym.Domain.Common.ErrorCodes;

public static class GeneralErrors
{
    public static Error TimeInvalid =>
        Error.Validation("General.TimeValidation", "Time range is invalid");
}