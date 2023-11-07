
using DomeGym.Domain.Common;

namespace DomeGym.Domain.SessionAggregate;

public class Reservation : EntityBase
{
    public Guid ParticipantId { get; }

    public Reservation(Guid participantId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        ParticipantId = participantId;
    }
}