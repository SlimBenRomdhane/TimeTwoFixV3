using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class upDateStockIncreaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "531f1867-9f8d-4909-b211-b7cf8a952781", new DateTime(2025, 9, 10, 22, 46, 12, 859, DateTimeKind.Utc).AddTicks(7495), "AQAAAAIAAYagAAAAEGdtjcff8HYbfQuV0Iz1yjI11tSN61esAmRXYh+zfC4lPIUyYCuM7w+M8Oi+6wUElg==", new DateTime(2025, 9, 10, 22, 46, 12, 859, DateTimeKind.Utc).AddTicks(7495) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 10, 22, 46, 12, 859, DateTimeKind.Utc).AddTicks(7171));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 10, 22, 46, 12, 859, DateTimeKind.Utc).AddTicks(7176));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 10, 22, 46, 12, 859, DateTimeKind.Utc).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 10, 22, 46, 12, 859, DateTimeKind.Utc).AddTicks(7178));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 10, 22, 46, 12, 859, DateTimeKind.Utc).AddTicks(7179));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
