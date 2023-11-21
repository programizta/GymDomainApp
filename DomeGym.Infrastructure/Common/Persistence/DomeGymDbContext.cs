using DomeGym.Domain.Common.Entities;
using DomeGym.Domain.GymAggregate;
using DomeGym.Domain.ParticipantAggregate;
using DomeGym.Domain.RoomAggregate;
using DomeGym.Domain.SessionAggregate;
using SubscriptionEntity = DomeGym.Domain.SubscriptionAggregate.Subscription;
using Microsoft.EntityFrameworkCore;
using DomeGym.Domain.TrainerAggregate;
using DomeGym.Domain.SubscriptionAggregate;
using DomeGym.Application.Common.Interfaces;
using System.Reflection;

namespace DomeGym.Infrastructure.Common.Persistence;

public class DomeGymDbContext : DbContext, IUnitOfWork
{
    public DbSet<Schedule> Schedules { get; set; } = null!;

    public DbSet<Gym> Gyms { get; set; } = null!;

    public DbSet<Participant> Participants { get; set; } = null!;

    public DbSet<Room> Rooms { get; set; } = null!;

    public DbSet<Session> Sessions { get; set; } = null!;

    public DbSet<SubscriptionEntity> Subscriptions { get; set; } = null!;

    public DbSet<Trainer> Trainers { get; set; } = null!;

    public DbSet<Reservation> Reservations { get; set; } = null!;

    public DbSet<SubscriptionDetails> SubscriptionDetails { get; set; } = null!;

    public DomeGymDbContext(DbContextOptions options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<SubscriptionDetails>().HasData(
            new SubscriptionDetails("Free", 1, 1, 1, 1),
            new SubscriptionDetails("Premium", -1, -1, -1, -1)
        );
    }

    public Task CommitChangesAsync()
    {
        return base.SaveChangesAsync();
    }
}