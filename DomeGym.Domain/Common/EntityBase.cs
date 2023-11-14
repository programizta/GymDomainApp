using DomeGym.Domain.Common.Interfaces;

namespace DomeGym.Domain.Common;

public abstract class EntityBase : IHasId
{
    public Guid Id { get; private set; }

    public EntityBase(Guid id)
    {
        Id = id;
    }

    public override bool Equals(object? otherEntity)
    {
        if (otherEntity is null || otherEntity.GetType() != GetType())
        {
            return false;
        }

        return ((EntityBase)otherEntity).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}