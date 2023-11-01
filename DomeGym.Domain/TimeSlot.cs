namespace DomeGym.Domain;

public class TimeSlot
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
}