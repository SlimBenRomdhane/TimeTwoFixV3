using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changingTheNameOfServiceToProvidedService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_Services_ServiceId",
                table: "Interventions");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Categories_CategoryId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "ProvidedServices");

            migrationBuilder.RenameIndex(
                name: "IX_Services_CategoryId",
                table: "ProvidedServices",
                newName: "IX_ProvidedServices_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProvidedServices",
                table: "ProvidedServices",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "a76b54c3-580c-437e-8ecf-1ab1a0f33c14", new DateTime(2025, 8, 5, 6, 18, 18, 155, DateTimeKind.Utc).AddTicks(831), "AQAAAAIAAYagAAAAEEbO3YngLYZISDs2z61TloXHNimi6vkE7fBlqL1KqUpPdkTfULftAsR2p+A2KRG4bA==", new DateTime(2025, 8, 5, 6, 18, 18, 155, DateTimeKind.Utc).AddTicks(831) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 5, 6, 18, 18, 155, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 5, 6, 18, 18, 155, DateTimeKind.Utc).AddTicks(545));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 5, 6, 18, 18, 155, DateTimeKind.Utc).AddTicks(547));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 5, 6, 18, 18, 155, DateTimeKind.Utc).AddTicks(548));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 5, 6, 18, 18, 155, DateTimeKind.Utc).AddTicks(549));

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_ProvidedServices_ServiceId",
                table: "Interventions",
                column: "ServiceId",
                principalTable: "ProvidedServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvidedServices_Categories_CategoryId",
                table: "ProvidedServices",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interventions_ProvidedServices_ServiceId",
                table: "Interventions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProvidedServices_Categories_CategoryId",
                table: "ProvidedServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProvidedServices",
                table: "ProvidedServices");

            migrationBuilder.RenameTable(
                name: "ProvidedServices",
                newName: "Services");

            migrationBuilder.RenameIndex(
                name: "IX_ProvidedServices_CategoryId",
                table: "Services",
                newName: "IX_Services_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "e5a4ba03-75cb-4d60-81ac-a80bdada7efd", new DateTime(2025, 8, 5, 6, 14, 2, 328, DateTimeKind.Utc).AddTicks(3731), "AQAAAAIAAYagAAAAEATF7FxkluZlAEbaEiy9Fo9g8pc5WAbCNNG8YB0ff1SehTj/f7Orhbuu7zjmI3KTgg==", new DateTime(2025, 8, 5, 6, 14, 2, 328, DateTimeKind.Utc).AddTicks(3732) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 5, 6, 14, 2, 328, DateTimeKind.Utc).AddTicks(3343));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 5, 6, 14, 2, 328, DateTimeKind.Utc).AddTicks(3349));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 5, 6, 14, 2, 328, DateTimeKind.Utc).AddTicks(3351));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 5, 6, 14, 2, 328, DateTimeKind.Utc).AddTicks(3352));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 5, 6, 14, 2, 328, DateTimeKind.Utc).AddTicks(3353));

            migrationBuilder.AddForeignKey(
                name: "FK_Interventions_Services_ServiceId",
                table: "Interventions",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Categories_CategoryId",
                table: "Services",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
