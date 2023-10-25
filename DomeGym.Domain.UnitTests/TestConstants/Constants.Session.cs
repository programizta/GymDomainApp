namespace DomeGym.Domain.UnitTests.TestConstants;

using System;

public static class Session
{
    public static readonly Guid Id = Guid.NewGuid();

    public const int MAX_NUMBER_OF_PARTICIPANTS = 1;
    
    public static readonly DateOnly Date = new DateOnly(DateTime.Now.Year,
        DateTime.Now.Month,
        DateTime.Now.Day);

    // let the StartTime be the time hour ago
    public static readonly TimeOnly StartTime = new TimeOnly(DateTime.Now.Hour - 1,
        DateTime.Now.Minute);

    // let the EndTime be the time hour after current time
    public static readonly TimeOnly EndTime = new TimeOnly(DateTime.Now.Hour + 1,
        DateTime.Now.Minute);
}