using DomeGym.Domain.ErrorCodes;
using ErrorOr;

namespace DomeGym.Domain;

public class Trainer
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new List<Guid>();
    private readonly Schedule _trainersSchedule;

    public Trainer(Guid userId)
    {
        _userId = userId;
        _trainersSchedule = new Schedule(userId);
    }

    public ErrorOr<Success> AssignSession(Session session)
    {
        if (session.StartTime >= session.EndTime)
        {
            return GeneralErrors.TimeInvalid;
        }

        if (IsSessionOverlapping(session))
        {
            return TrainerErrors.CannotAssignOverlappingSessionToTrainer;
        }

        _trainersSchedule.BookSession(session.Date, session.StartTime, session.EndTime);
        _sessionIds.Add(session.Id);

        return Result.Success;
    }

    private bool IsSessionOverlapping(Session session)
    {
        // if there is no already booked session for this day, return false immediately (no overlap)
        if (!_trainersSchedule.TimeSlotsForDays.TryGetValue(session.Date, out var timeSlots))
        {
            return false;
        }

        var potentialNewSession = new TimeSlot(session.StartTime, session.EndTime);
        return timeSlots.Any(x => !x.CanBookTimeSlot(potentialNewSession));
    }
}
