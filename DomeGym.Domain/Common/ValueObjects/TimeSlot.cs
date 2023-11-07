
namespace DomeGym.Domain.Common.ValueObjects;

/// <summary>
/// By creating "TimeSlot" class, we introduce coupling between different Entities/Aggregates (e.g. Schedule, Participant),
/// so we need to note that this object cannot be too entity-specific in order not to have dependencies
/// and allow to evolve and grow this object independently from other classes.
/// </summary>
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