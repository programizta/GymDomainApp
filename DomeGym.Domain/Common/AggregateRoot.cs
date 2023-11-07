
namespace DomeGym.Domain.Common;

public abstract class AggregateRoot : EntityBase
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }
}