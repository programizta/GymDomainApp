
using DomeGym.Domain.Common;

namespace DomeGym.Domain.SessionAggregate;

public class Reservation : EntityBase
{
    public Guid ParticipantId { get; private set; }

    public Reservation()
        : base(Guid.NewGuid())
    {
        
    }

    public Reservation(Guid participantId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        ParticipantId = participantId;
    }
}