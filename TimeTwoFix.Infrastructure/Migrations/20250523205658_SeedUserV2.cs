using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47f3a602-5f32-4413-bd95-73da218b5a40", new DateTime(2025, 5, 23, 20, 56, 57, 934, DateTimeKind.Utc).AddTicks(9031), "AQAAAAIAAYagAAAAEN06S0sFdfI5OM7Q128mVf2fxPvjukMkwdomIeaFRZmdIcKDNJ5nk20VPtoCB+8RLA==", "00000000-0000-0000-0000-000000000000" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 56, 57, 934, DateTimeKind.Utc).AddTicks(8754));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 56, 57, 934, DateTimeKind.Utc).AddTicks(8760));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 56, 57, 934, DateTimeKind.Utc).AddTicks(8762));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 56, 57, 934, DateTimeKind.Utc).AddTicks(8762));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 23, 20, 56, 57, 934, DateTimeKind.Utc).AddTicks(8763));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ffd1535b-c0cd-4a10-a4e6-d65eee913c6e", new DateTime(2025, 5, 23, 20, 52, 48, 860, DateTimeKind.Utc).AddTicks(164), "AQAAAAIAAYagAAAAEGDX56u31FZiyYEj7YZBwgC0M6/Bbs25s8Iejw6vCX8zKFFnOGBctU43jlkLH1hl4A==", null });

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
    }
}
