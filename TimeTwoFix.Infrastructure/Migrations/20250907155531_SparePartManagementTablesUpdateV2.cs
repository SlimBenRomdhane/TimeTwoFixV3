using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SparePartManagementTablesUpdateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderSparePart_Provider_ProviderId",
                table: "ProviderSparePart");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderSparePart_SpareParts_SparePartId",
                table: "ProviderSparePart");

            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_SparePartCategory_SparePartCategoryId",
                table: "SpareParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SparePartCategory",
                table: "SparePartCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProviderSparePart",
                table: "ProviderSparePart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provider",
                table: "Provider");

            migrationBuilder.RenameTable(
                name: "SparePartCategory",
                newName: "SparePartCategories");

            migrationBuilder.RenameTable(
                name: "ProviderSparePart",
                newName: "StockIncrease");

            migrationBuilder.RenameTable(
                name: "Provider",
                newName: "Providers");

            migrationBuilder.RenameIndex(
                name: "IX_ProviderSparePart_SparePartId",
                table: "StockIncrease",
                newName: "IX_StockIncrease_SparePartId");

            migrationBuilder.RenameIndex(
                name: "IX_ProviderSparePart_ProviderId",
                table: "StockIncrease",
                newName: "IX_StockIncrease_ProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SparePartCategories",
                table: "SparePartCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockIncrease",
                table: "StockIncrease",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Providers",
                table: "Providers",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_SparePartCategories_SparePartCategoryId",
                table: "SpareParts",
                column: "SparePartCategoryId",
                principalTable: "SparePartCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockIncrease_Providers_ProviderId",
                table: "StockIncrease",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockIncrease_SpareParts_SparePartId",
                table: "StockIncrease",
                column: "SparePartId",
                principalTable: "SpareParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_SparePartCategories_SparePartCategoryId",
                table: "SpareParts");

            migrationBuilder.DropForeignKey(
                name: "FK_StockIncrease_Providers_ProviderId",
                table: "StockIncrease");

            migrationBuilder.DropForeignKey(
                name: "FK_StockIncrease_SpareParts_SparePartId",
                table: "StockIncrease");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockIncrease",
                table: "StockIncrease");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SparePartCategories",
                table: "SparePartCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Providers",
                table: "Providers");

            migrationBuilder.RenameTable(
                name: "StockIncrease",
                newName: "ProviderSparePart");

            migrationBuilder.RenameTable(
                name: "SparePartCategories",
                newName: "SparePartCategory");

            migrationBuilder.RenameTable(
                name: "Providers",
                newName: "Provider");

            migrationBuilder.RenameIndex(
                name: "IX_StockIncrease_SparePartId",
                table: "ProviderSparePart",
                newName: "IX_ProviderSparePart_SparePartId");

            migrationBuilder.RenameIndex(
                name: "IX_StockIncrease_ProviderId",
                table: "ProviderSparePart",
                newName: "IX_ProviderSparePart_ProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProviderSparePart",
                table: "ProviderSparePart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SparePartCategory",
                table: "SparePartCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provider",
                table: "Provider",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "08481ab1-79ac-400b-a280-5d2d22419a19", new DateTime(2025, 9, 7, 15, 50, 43, 34, DateTimeKind.Utc).AddTicks(8141), "AQAAAAIAAYagAAAAENFIB9soaEZecxS4KKL4wCruRuKIcUP/ZGYRQyr4OVi6OIdM4i7GjBxKqjGzxyInYg==", new DateTime(2025, 9, 7, 15, 50, 43, 34, DateTimeKind.Utc).AddTicks(8141) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 15, 50, 43, 34, DateTimeKind.Utc).AddTicks(7788));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 15, 50, 43, 34, DateTimeKind.Utc).AddTicks(7794));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 15, 50, 43, 34, DateTimeKind.Utc).AddTicks(7795));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 15, 50, 43, 34, DateTimeKind.Utc).AddTicks(7796));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 7, 15, 50, 43, 34, DateTimeKind.Utc).AddTicks(7798));

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderSparePart_Provider_ProviderId",
                table: "ProviderSparePart",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderSparePart_SpareParts_SparePartId",
                table: "ProviderSparePart",
                column: "SparePartId",
                principalTable: "SpareParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_SparePartCategory_SparePartCategoryId",
                table: "SpareParts",
                column: "SparePartCategoryId",
                principalTable: "SparePartCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
