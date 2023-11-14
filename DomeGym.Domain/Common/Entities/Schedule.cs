using System.ComponentModel.DataAnnotations.Schema;
using DomeGym.Domain.Common.ValueObjects;

namespace DomeGym.Domain.Common.Entities;

public class Schedule : EntityBase
{
    public Dictionary<DateOnly, List<TimeSlot>> TimeSlotsForDays { get; }

    public Schedule()
     : base(Guid.NewGuid())
    {
        
    }

    public Schedule(Guid? scheduleId = null)
        : base(scheduleId ?? Guid.NewGuid())
    {
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