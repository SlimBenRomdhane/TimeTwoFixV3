using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingVehicleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Vin",
                table: "Vehicles",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(17)",
                oldMaxLength: 17);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "36ea0511-036d-488e-aeae-932bca0783a6", new DateTime(2025, 5, 30, 15, 20, 47, 871, DateTimeKind.Utc).AddTicks(142), "AQAAAAIAAYagAAAAENRVfadxmlIp9LKvjuoWl0IVmN+IR0iSuvHiiOmFjG07ACqzxmxPyQQ2T2ppjXrusg==", new DateTime(2025, 5, 30, 15, 20, 47, 871, DateTimeKind.Utc).AddTicks(143) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 20, 47, 870, DateTimeKind.Utc).AddTicks(9926));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 20, 47, 870, DateTimeKind.Utc).AddTicks(9931));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 20, 47, 870, DateTimeKind.Utc).AddTicks(9932));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 20, 47, 870, DateTimeKind.Utc).AddTicks(9933));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 20, 47, 870, DateTimeKind.Utc).AddTicks(9934));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Vin",
                table: "Vehicles",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "1b38045b-d314-48fa-9ea4-8d4c2c687679", new DateTime(2025, 5, 30, 11, 27, 7, 341, DateTimeKind.Utc).AddTicks(9706), "AQAAAAIAAYagAAAAEAiYO6+1WsVHrH8kOD18HAgifZRsA8Y5RLwWFjRZwYuyNy1R9GJDpPS5bFE3gzDOGQ==", new DateTime(2025, 5, 30, 11, 27, 7, 341, DateTimeKind.Utc).AddTicks(9706) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 11, 27, 7, 341, DateTimeKind.Utc).AddTicks(9464));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 11, 27, 7, 341, DateTimeKind.Utc).AddTicks(9469));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 11, 27, 7, 341, DateTimeKind.Utc).AddTicks(9471));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 11, 27, 7, 341, DateTimeKind.Utc).AddTicks(9472));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 11, 27, 7, 341, DateTimeKind.Utc).AddTicks(9473));
        }
    }
}
