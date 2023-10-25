namespace DomeGym.Domain.Interfaces;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}