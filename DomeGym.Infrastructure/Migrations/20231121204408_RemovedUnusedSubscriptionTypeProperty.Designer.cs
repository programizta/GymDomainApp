﻿// <auto-generated />
using System;
using DomeGym.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DomeGym.Infrastructure.Migrations
{
    [DbContext(typeof(DomeGymDbContext))]
    [Migration("20231121204408_RemovedUnusedSubscriptionTypeProperty")]
    partial class RemovedUnusedSubscriptionTypeProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("DomeGym.Domain.Common.Entities.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("DomeGym.Domain.GymAggregate.Gym", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxNumberOfRooms")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Gyms");
                });

            modelBuilder.Entity("DomeGym.Domain.ParticipantAggregate.Participant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("DomeGym.Domain.RoomAggregate.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxNumberOfSessions")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("DomeGym.Domain.SessionAggregate.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SessionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("DomeGym.Domain.SessionAggregate.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaximumNumberOfParticipants")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("DomeGym.Domain.SubscriptionAggregate.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubscriptionDetailsId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionDetailsId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("DomeGym.Domain.SubscriptionAggregate.SubscriptionDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxNumberOfGyms")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxNumberOfGymsAllowed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxNumberOfRoomsInGym")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxNumberOfSessionsInRoom")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionDetails");
                });

            modelBuilder.Entity("DomeGym.Domain.TrainerAggregate.Trainer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TrainersScheduleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TrainersScheduleId");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("DomeGym.Domain.ParticipantAggregate.Participant", b =>
                {
                    b.HasOne("DomeGym.Domain.Common.Entities.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("DomeGym.Domain.RoomAggregate.Room", b =>
                {
                    b.HasOne("DomeGym.Domain.Common.Entities.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("DomeGym.Domain.SessionAggregate.Reservation", b =>
                {
                    b.HasOne("DomeGym.Domain.SessionAggregate.Session", null)
                        .WithMany("Reservations")
                        .HasForeignKey("SessionId");
                });

            modelBuilder.Entity("DomeGym.Domain.SubscriptionAggregate.Subscription", b =>
                {
                    b.HasOne("DomeGym.Domain.SubscriptionAggregate.SubscriptionDetails", "SubscriptionDetails")
                        .WithMany()
                        .HasForeignKey("SubscriptionDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubscriptionDetails");
                });

            modelBuilder.Entity("DomeGym.Domain.TrainerAggregate.Trainer", b =>
                {
                    b.HasOne("DomeGym.Domain.Common.Entities.Schedule", "TrainersSchedule")
                        .WithMany()
                        .HasForeignKey("TrainersScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainersSchedule");
                });

            modelBuilder.Entity("DomeGym.Domain.SessionAggregate.Session", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
