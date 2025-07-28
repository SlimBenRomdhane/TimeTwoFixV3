using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class justAnotherUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
