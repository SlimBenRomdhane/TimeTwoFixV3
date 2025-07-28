using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class someAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "InstallationDate",
                table: "LiftingBridges",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "7fc9ec02-03c1-46fe-ae75-19be8f4aa887", new DateTime(2025, 7, 23, 6, 23, 19, 839, DateTimeKind.Utc).AddTicks(9754), "AQAAAAIAAYagAAAAEKc3z4CgAwxt3JiPtsBi4MG7MDsW1GG4vZzQczFiCRbMMxmddbP5B6HiwvFN731Qsw==", new DateTime(2025, 7, 23, 6, 23, 19, 839, DateTimeKind.Utc).AddTicks(9755) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 6, 23, 19, 839, DateTimeKind.Utc).AddTicks(9450));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 6, 23, 19, 839, DateTimeKind.Utc).AddTicks(9458));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 6, 23, 19, 839, DateTimeKind.Utc).AddTicks(9459));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 6, 23, 19, 839, DateTimeKind.Utc).AddTicks(9460));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 23, 6, 23, 19, 839, DateTimeKind.Utc).AddTicks(9461));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InstallationDate",
                table: "LiftingBridges",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "a4599d9b-7c28-4c1b-b073-275d7bb00a97", new DateTime(2025, 7, 20, 15, 23, 43, 964, DateTimeKind.Utc).AddTicks(7712), "AQAAAAIAAYagAAAAEHe5i9q0HzzUqU0sKlVG+CLyGxfdWv22S2i5t+5cf1XahESUxe1qVWT0Fkj5kFgRrA==", new DateTime(2025, 7, 20, 15, 23, 43, 964, DateTimeKind.Utc).AddTicks(7713) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 20, 15, 23, 43, 964, DateTimeKind.Utc).AddTicks(7446));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 20, 15, 23, 43, 964, DateTimeKind.Utc).AddTicks(7451));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 20, 15, 23, 43, 964, DateTimeKind.Utc).AddTicks(7452));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 20, 15, 23, 43, 964, DateTimeKind.Utc).AddTicks(7453));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 20, 15, 23, 43, 964, DateTimeKind.Utc).AddTicks(7454));
        }
    }
}
