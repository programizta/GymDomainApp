namespace DomeGym.Domain.UnitTests.TestConstants;

using System;

public static class Session
{
    public static readonly Guid Id = Guid.NewGuid();

    public const int MAX_NUMBER_OF_PARTICIPANTS = 1;
    
    public static readonly DateOnly Date = new DateOnly(DateTime.Now.Year,
        DateTime.Now.Month,
        DateTime.Now.Day);

    // let the StartTime1 be current time
    public static readonly TimeOnly StartTime1 = new TimeOnly(DateTime.Now.Hour,
        DateTime.Now.Minute);

    // let the EndTime1 be the time hour after current time
    public static readonly TimeOnly EndTime1 = new TimeOnly(DateTime.Now.Hour + 1,
        DateTime.Now.Minute);

    // let the StartTime2 be an hour later after EndTime1
    public static readonly TimeOnly StartTime2 = new TimeOnly(EndTime1.Hour + 1,
        EndTime1.Minute);

    // let the EndTime2 be two hours later after EndTime1
    public static readonly TimeOnly EndTime2 = new TimeOnly(EndTime1.Hour + 2,
        EndTime1.Minute);
}