using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DomeGym.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubscriptionTypeName",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "SubscriptionDetails",
                columns: new[] { "Id", "MaxNumberOfGyms", "MaxNumberOfGymsAllowed", "MaxNumberOfRoomsInGym", "MaxNumberOfSessionsInRoom", "SubscriptionName" },
                values: new object[,]
                {
                    { new Guid("24b574ca-faf7-4555-af7f-b1011d9b1160"), -1, -1, -1, -1, "Premium" },
                    { new Guid("380428d2-ecf5-437a-b725-d580b00784ac"), 1, 1, 1, 1, "Free" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionDetails",
                keyColumn: "Id",
                keyValue: new Guid("24b574ca-faf7-4555-af7f-b1011d9b1160"));

            migrationBuilder.DeleteData(
                table: "SubscriptionDetails",
                keyColumn: "Id",
                keyValue: new Guid("380428d2-ecf5-437a-b725-d580b00784ac"));

            migrationBuilder.DropColumn(
                name: "SubscriptionTypeName",
                table: "Subscriptions");
        }
    }
}
