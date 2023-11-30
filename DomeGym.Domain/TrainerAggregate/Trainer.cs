using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.TrainerAggregate;

public class Trainer : AggregateRoot
{
    public List<Guid> SessionIds { get; }

    public Schedule TrainersSchedule { get; private set; }

    public Trainer()
     : base(Guid.NewGuid())
    {

    }

    public Trainer(Guid? trainerId = null)
        : base(trainerId ?? Guid.NewGuid())
    {
        TrainersSchedule = new Schedule(scheduleId: Guid.NewGuid());
        SessionIds = new List<Guid>();
    }

    public ErrorOr<Success> AssignSession(Session session)
    {
        // TODO: implement nicer way of handing the exception while adding
        // existing session to the SessionIds
        if (SessionIds.Any(x => x == session.Id))
        {
            throw new Exception();
        }

        if (IsSessionOverlapping(session))
        {
            return TrainerErrors.CannotAssignOverlappingSessionToTrainer;
        }

        TrainersSchedule.BookSession(session.Date, session.StartTime, session.EndTime);
        SessionIds.Add(session.Id);

        return Result.Success;
    }

    private bool IsSessionOverlapping(Session session)
    {
        // if there is no already booked session for this day, return false immediately (no overlap)
        if (!TrainersSchedule.TimeSlotsForDays.TryGetValue(session.Date, out var timeSlots))
        {
            return false;
        }

        var potentialNewSession = new TimeSlot(session.StartTime, session.EndTime);
        return timeSlots.Any(x => !x.CanBookTimeSlot(potentialNewSession));
    }
}
