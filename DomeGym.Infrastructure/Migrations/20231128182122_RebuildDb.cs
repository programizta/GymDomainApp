using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DomeGym.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RebuildDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gyms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MaxNumberOfRooms = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MaximumNumberOfParticipants = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubscriptionName = table.Column<string>(type: "TEXT", nullable: false),
                    MaxNumberOfGyms = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxNumberOfRoomsInGym = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxNumberOfSessionsInRoom = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxNumberOfGymsAllowed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MaxNumberOfSessions = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TrainersScheduleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_Schedules_TrainersScheduleId",
                        column: x => x.TrainersScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SessionId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AdminId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubscriptionDetailsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_SubscriptionDetails_SubscriptionDetailsId",
                        column: x => x.SubscriptionDetailsId,
                        principalTable: "SubscriptionDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SubscriptionDetails",
                columns: new[] { "Id", "MaxNumberOfGyms", "MaxNumberOfGymsAllowed", "MaxNumberOfRoomsInGym", "MaxNumberOfSessionsInRoom", "SubscriptionName" },
                values: new object[,]
                {
                    { new Guid("71a2f665-6eff-427e-8ef4-bc385ffb15e8"), -1, -1, -1, -1, "Premium" },
                    { new Guid("8b1ac7c1-7c44-41d0-a657-370bb948ea35"), 1, 1, 1, 1, "Free" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ScheduleId",
                table: "Participants",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SessionId",
                table: "Reservations",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ScheduleId",
                table: "Rooms",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionDetailsId",
                table: "Subscriptions",
                column: "SubscriptionDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_TrainersScheduleId",
                table: "Trainers",
                column: "TrainersScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gyms");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "SubscriptionDetails");

            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
