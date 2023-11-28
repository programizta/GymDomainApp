using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using DomeGym.Domain.SubscriptionAggregate;

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

    public async Task<bool> DeleteSubscriptionByIdAsync(Guid subscriptionId)
    {
        bool success = false;
        var subscriptionToRemove = await GetSubscriptionByIdAsync(subscriptionId);

        if (subscriptionToRemove is not null)
        {
            // first remove depending SubscriptionDetails from Subscription and then the Subscription itself
            //subscriptionToRemove.SubscriptionDetails = null;
            _dbContext.Subscriptions.Remove(subscriptionToRemove);
            success = true;
        }

        return success;
    }

    public async Task<SubscriptionEntity?> GetSubscriptionByIdAsync(Guid subscriptionId)
    {
        var subscription = await _dbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == subscriptionId);

        if (subscription is not null)
        {
            var subscriptionDetailsToHydrateSubscription = await _dbContext.SubscriptionDetails.FirstOrDefaultAsync(x => x.Id == subscription.SubscriptionDetailsId);

            if (subscriptionDetailsToHydrateSubscription is not null)
            {
                subscription.AssignDetailsToSubscription(subscriptionDetailsToHydrateSubscription);
            }
        }

        return subscription;
    }

    public Task<SubscriptionDetails?> GetSubscriptionDetailsByName(string subscriptionName) =>
        _dbContext.SubscriptionDetails.FirstOrDefaultAsync(x => x.SubscriptionName == subscriptionName);
}