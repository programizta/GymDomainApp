using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Infrastructure.Common.Persistence;

namespace DomeGym.Infrastructure.Subscription.Persistence;

public class SubscriptionRepository : ISubscriptionRespository
{
    private readonly DomeGymDbContext _dbContext;

    public SubscriptionRepository(DomeGymDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSubscriptionAsync(SubscriptionEntity subscriptionEntity)
    {
        await _dbContext.Subscriptions.AddAsync(subscriptionEntity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<SubscriptionEntity?> GetSubscriptionByIdAsync(Guid subscriptionId) =>
        Task.FromResult(_dbContext.Subscriptions.FirstOrDefault(x => x.Id == subscriptionId));
}