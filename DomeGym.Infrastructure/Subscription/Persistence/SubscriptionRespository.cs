using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

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
    }

    public async Task<SubscriptionEntity?> GetSubscriptionByIdAsync(Guid subscriptionId)
    {
        var subscription = await _dbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == subscriptionId);

        if (subscription is not null)
        {
            var subscriptionDetailsToHydrateSubscription = await _dbContext.SubscriptionDetails.FirstOrDefaultAsync(x => x.Id == subscription.SubscriptionDetailsId);

            if (subscriptionDetailsToHydrateSubscription is not null)
            {
                subscription.FillSubscriptionWithDetails(subscriptionDetailsToHydrateSubscription);
            }
        }

        return subscription;
    }
}