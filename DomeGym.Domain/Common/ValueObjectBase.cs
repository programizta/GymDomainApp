namespace DomeGym.Domain.Common;

public abstract class ValueObjectBase
{
    public abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? otherValueObject)
    {
        if (otherValueObject is null || otherValueObject.GetType() != GetType())
        {
            return false;
        }

        return ((ValueObjectBase)otherValueObject)
            .GetEqualityComponents()
            .SequenceEqual(GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return  GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}