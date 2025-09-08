using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SparePartTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "SpareParts");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "8b0b8f23-55b6-4e95-81c7-1ce41de082dd", new DateTime(2025, 9, 5, 6, 11, 44, 708, DateTimeKind.Utc).AddTicks(9977), "AQAAAAIAAYagAAAAEDr6Na4aNYDZ8chUUxMby3bCNcsfkUkNbUxDGmaALTWULpaBRKoQ7M9nExNpFCY1Pw==", new DateTime(2025, 9, 5, 6, 11, 44, 708, DateTimeKind.Utc).AddTicks(9977) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 5, 6, 11, 44, 708, DateTimeKind.Utc).AddTicks(9757));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 5, 6, 11, 44, 708, DateTimeKind.Utc).AddTicks(9763));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 5, 6, 11, 44, 708, DateTimeKind.Utc).AddTicks(9764));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 5, 6, 11, 44, 708, DateTimeKind.Utc).AddTicks(9765));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 5, 6, 11, 44, 708, DateTimeKind.Utc).AddTicks(9767));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "SpareParts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "ff45074b-7901-4937-ab8d-ffd1048b521e", new DateTime(2025, 9, 5, 6, 9, 41, 224, DateTimeKind.Utc).AddTicks(4478), "AQAAAAIAAYagAAAAEPUK/FL1Sh1VZlR34B+/sIIM6xmcoDu/3J/1z6ivVtblYae68oZs5Drhwz+ndkpBoA==", new DateTime(2025, 9, 5, 6, 9, 41, 224, DateTimeKind.Utc).AddTicks(4478) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 5, 6, 9, 41, 224, DateTimeKind.Utc).AddTicks(4172));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 5, 6, 9, 41, 224, DateTimeKind.Utc).AddTicks(4178));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 5, 6, 9, 41, 224, DateTimeKind.Utc).AddTicks(4180));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 5, 6, 9, 41, 224, DateTimeKind.Utc).AddTicks(4181));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 5, 6, 9, 41, 224, DateTimeKind.Utc).AddTicks(4183));
        }
    }
}