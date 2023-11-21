using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DomeGym.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SubscriptionDetails",
                columns: new[] { "Id", "MaxNumberOfGyms", "MaxNumberOfGymsAllowed", "MaxNumberOfRoomsInGym", "MaxNumberOfSessionsInRoom", "SubscriptionName" },
                values: new object[,]
                {
                    { new Guid("60829e92-8a55-48a3-95f8-d5a3510eee3e"), -1, -1, -1, -1, "Premium" },
                    { new Guid("f1e069da-c0cc-4a26-a1a9-90a4cc767394"), 1, 1, 1, 1, "Free" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionDetails",
                keyColumn: "Id",
                keyValue: new Guid("60829e92-8a55-48a3-95f8-d5a3510eee3e"));

            migrationBuilder.DeleteData(
                table: "SubscriptionDetails",
                keyColumn: "Id",
                keyValue: new Guid("f1e069da-c0cc-4a26-a1a9-90a4cc767394"));
        }
    }
}
