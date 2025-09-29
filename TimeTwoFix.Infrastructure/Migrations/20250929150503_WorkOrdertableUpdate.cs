using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WorkOrdertableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "WorkOrders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "WorkOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "WorkOrders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "77119633-8cdf-4161-8a6d-c0deddc2e244", new DateTime(2025, 9, 29, 15, 5, 1, 329, DateTimeKind.Utc).AddTicks(450), "AQAAAAIAAYagAAAAEOwNPYeZ575XaeUDrarqA0QB91+Zzwtc+YImdIyMo3IdNwpZ1qJ+H8gYatXZmBDIew==", new DateTime(2025, 9, 29, 15, 5, 1, 329, DateTimeKind.Utc).AddTicks(452) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 29, 15, 5, 1, 329, DateTimeKind.Utc).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 29, 15, 5, 1, 329, DateTimeKind.Utc).AddTicks(22));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 29, 15, 5, 1, 329, DateTimeKind.Utc).AddTicks(24));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 29, 15, 5, 1, 329, DateTimeKind.Utc).AddTicks(25));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 29, 15, 5, 1, 329, DateTimeKind.Utc).AddTicks(27));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "WorkOrders",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "WorkOrders",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "WorkOrders",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "WorkOrders",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "14f1dc93-3ff3-43a7-bd9c-ff0f6af9be6b", new DateTime(2025, 9, 13, 17, 54, 23, 45, DateTimeKind.Utc).AddTicks(4239), "AQAAAAIAAYagAAAAEPK95e/E1bmqF99hEPlPkLTRpm1BhTWN+BPw7zWGDFDarc5tV66YXBZOI+phC/l0CA==", new DateTime(2025, 9, 13, 17, 54, 23, 45, DateTimeKind.Utc).AddTicks(4240) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 13, 17, 54, 23, 45, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 13, 17, 54, 23, 45, DateTimeKind.Utc).AddTicks(3866));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 13, 17, 54, 23, 45, DateTimeKind.Utc).AddTicks(3867));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 13, 17, 54, 23, 45, DateTimeKind.Utc).AddTicks(3868));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 13, 17, 54, 23, 45, DateTimeKind.Utc).AddTicks(3869));
        }
    }
}
