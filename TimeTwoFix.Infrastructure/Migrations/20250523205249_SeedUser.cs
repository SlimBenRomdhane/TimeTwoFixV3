using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "City", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "HireDate", "HourlyWage", "ImageUrl", "LastEmployer", "LastName", "NormalizedEmail", "NormalizedUserName", "OfficeNumber", "PasswordHash", "PhoneNumber", "RoleTypeDiscriminator", "SecurityStamp", "Status", "UpdatedAt", "UserName", "YearsInManagement", "YearsOfExperience", "ZipCode" },
                values: new object[] { 1, "admin", "admin", "ffd1535b-c0cd-4a10-a4e6-d65eee913c6e", new DateTime(2025, 5, 23, 20, 52, 48, 860, DateTimeKind.Utc).AddTicks(164), "admin@admin.com", false, "admin", new DateOnly(1, 1, 1), 0m, "admin", "admin", "admin", null, "ADMIN", null, "AQAAAAIAAYagAAAAEGDX56u31FZiyYEj7YZBwgC0M6/Bbs25s8Iejw6vCX8zKFFnOGBctU43jlkLH1hl4A==", null, "GeneralManager", null, "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 52, 48, 859, DateTimeKind.Utc).AddTicks(9785));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 52, 48, 859, DateTimeKind.Utc).AddTicks(9791));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 52, 48, 859, DateTimeKind.Utc).AddTicks(9793));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 52, 48, 859, DateTimeKind.Utc).AddTicks(9795));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 52, 48, 859, DateTimeKind.Utc).AddTicks(9796));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 2, 4, 985, DateTimeKind.Utc).AddTicks(5662));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 2, 4, 985, DateTimeKind.Utc).AddTicks(5668));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 2, 4, 985, DateTimeKind.Utc).AddTicks(5669));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 2, 4, 985, DateTimeKind.Utc).AddTicks(5670));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 2, 4, 985, DateTimeKind.Utc).AddTicks(5671));
        }
    }
}
