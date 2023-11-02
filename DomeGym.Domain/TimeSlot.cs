using DomeGym.Domain.Common;

namespace DomeGym.Domain;

public class TimeSlot : ValueObjectBase
{
    public TimeOnly StartTime { get; }

    public TimeOnly EndTime { get; }

    public TimeSlot(TimeOnly startTime, TimeOnly endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public bool CanBookTimeSlot(TimeSlot eventToBeBooked)
    {
        return eventToBeBooked.EndTime <= StartTime
            || eventToBeBooked.StartTime >= EndTime;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StartTime;
        yield return EndTime;
    }
}