using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Infrastructure.Subscription.Persistence;

public class SubscriptionConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
{
    public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.SubscriptionDetailsId)
            .ValueGeneratedNever();
    }
}
