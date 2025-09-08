using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SparePartManagementTablesUpdateV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "Providers",
                newName: "MobileContactPhone");

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FiscalId",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LandContactPhone",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RIB",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "FiscalId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "LandContactPhone",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "RIB",
                table: "Providers");

            migrationBuilder.RenameColumn(
                name: "MobileContactPhone",
                table: "Providers",
                newName: "ContactPhone");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "ac922ef1-bf7a-4a7d-ae31-bb3ea5a4bc70", new DateTime(2025, 9, 7, 15, 55, 30, 252, DateTimeKind.Utc).AddTicks(6997), "AQAAAAIAAYagAAAAEPnTqApnkqofqAjyyl4zWhvV4CYPj0toVy7MMnMHS1c++x87N8F1jXk02TbpILvrCg==", new DateTime(2025, 9, 7, 15, 55, 30, 252, DateTimeKind.Utc).AddTicks(6997) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 15, 55, 30, 252, DateTimeKind.Utc).AddTicks(6634));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 15, 55, 30, 252, DateTimeKind.Utc).AddTicks(6639));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 15, 55, 30, 252, DateTimeKind.Utc).AddTicks(6640));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 15, 55, 30, 252, DateTimeKind.Utc).AddTicks(6641));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 15, 55, 30, 252, DateTimeKind.Utc).AddTicks(6642));
        }
    }
}