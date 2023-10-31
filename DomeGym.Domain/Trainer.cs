
namespace DomeGym.Domain;

public class Trainer
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds;

    public Trainer(Guid userId)
    {
        _userId = userId;
    }
}
