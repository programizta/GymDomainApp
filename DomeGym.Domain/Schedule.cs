using DomeGym.Domain.Interfaces;

namespace DomeGym.Domain;

public class Schedule : IHasId
{
    public Dictionary<DateOnly, List<TimeSlot>> TimeSlotsForDays { get; }

    public Guid Id { get; }

    public Schedule(Guid id)
    {
        Id = id;
        TimeSlotsForDays = new Dictionary<DateOnly, List<TimeSlot>>();
    }

    public void BookSession(
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime)
    {
        var newTimeSlot = new TimeSlot(startTime, endTime);

        if (!TimeSlotsForDays.TryGetValue(date, out var listOfSlots))
        {
            var newListOfSlots = new List<TimeSlot> { newTimeSlot };

            TimeSlotsForDays.Add(date, newListOfSlots);
        }
        else
        {
            listOfSlots.Add(newTimeSlot);
        }
    }
}