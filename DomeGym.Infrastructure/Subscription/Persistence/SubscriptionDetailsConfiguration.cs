using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomeGym.Domain.SubscriptionAggregate;

namespace DomeGym.Infrastructure.Subscription.Persistence;

public class SubscriptionDetailsConfiguration : IEntityTypeConfiguration<SubscriptionDetails>
{
    public void Configure(EntityTypeBuilder<SubscriptionDetails> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}
