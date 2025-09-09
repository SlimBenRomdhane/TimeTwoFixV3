using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestAzure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryReceipt",
                table: "StockIncrease",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "126e998d-233f-4a8f-a145-ed61b89c1d54", new DateTime(2025, 9, 8, 13, 57, 19, 996, DateTimeKind.Utc).AddTicks(3110), "AQAAAAIAAYagAAAAEN2mGp63U7yVgjX4j9rbZC+4fB6JXhJbf2hZcyAlk80xIBvYZW5MGIHipvATbYqP+g==", new DateTime(2025, 9, 8, 13, 57, 19, 996, DateTimeKind.Utc).AddTicks(3110) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 8, 13, 57, 19, 996, DateTimeKind.Utc).AddTicks(2703));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 8, 13, 57, 19, 996, DateTimeKind.Utc).AddTicks(2713));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 8, 13, 57, 19, 996, DateTimeKind.Utc).AddTicks(2714));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 8, 13, 57, 19, 996, DateTimeKind.Utc).AddTicks(2716));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 8, 13, 57, 19, 996, DateTimeKind.Utc).AddTicks(2717));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryReceipt",
                table: "StockIncrease");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "4fcb9d6a-5f7b-495b-9cfd-7b648db12dd5", new DateTime(2025, 9, 7, 16, 8, 26, 350, DateTimeKind.Utc).AddTicks(3655), "AQAAAAIAAYagAAAAEKx6VA7RmH/NdJsN0fG+QkWezZmIKxbBhXSsCiIuSjAHvZ6dCmNIzTxFbU9HD4j6Jw==", new DateTime(2025, 9, 7, 16, 8, 26, 350, DateTimeKind.Utc).AddTicks(3656) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 16, 8, 26, 350, DateTimeKind.Utc).AddTicks(3405));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 16, 8, 26, 350, DateTimeKind.Utc).AddTicks(3409));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 16, 8, 26, 350, DateTimeKind.Utc).AddTicks(3410));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 16, 8, 26, 350, DateTimeKind.Utc).AddTicks(3411));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 16, 8, 26, 350, DateTimeKind.Utc).AddTicks(3412));
        }
    }
}
