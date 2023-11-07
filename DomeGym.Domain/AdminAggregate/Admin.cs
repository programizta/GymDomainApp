using DomeGym.Domain.Common;

namespace DomeGym.Domain.AdminAggregate;
public class Admin : AggregateRoot
{
    private readonly Guid _userId;
    private readonly Guid _subscriptionId;

    public Admin(Guid userId, Guid subscriptionId, Guid? adminId = null)
        : base(adminId ?? Guid.NewGuid())
    {
        _userId = userId;
        _subscriptionId = subscriptionId;
    }
}
