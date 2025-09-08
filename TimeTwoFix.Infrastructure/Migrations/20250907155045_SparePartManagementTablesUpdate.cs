using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SparePartManagementTablesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterventionSpareParts_Interventions_InterventionId",
                table: "InterventionSpareParts");

            migrationBuilder.DropForeignKey(
                name: "FK_InterventionSpareParts_SpareParts_SparePartId",
                table: "InterventionSpareParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterventionSpareParts",
                table: "InterventionSpareParts");

            migrationBuilder.RenameTable(
                name: "InterventionSpareParts",
                newName: "StockDecrease");

            migrationBuilder.RenameIndex(
                name: "IX_InterventionSpareParts_SparePartId",
                table: "StockDecrease",
                newName: "IX_StockDecrease_SparePartId");

            migrationBuilder.RenameIndex(
                name: "IX_InterventionSpareParts_InterventionId",
                table: "StockDecrease",
                newName: "IX_StockDecrease_InterventionId");

            migrationBuilder.RenameIndex(
                name: "IX_InterventionSpareParts_DeliveryNote",
                table: "StockDecrease",
                newName: "IX_StockDecrease_DeliveryNote");

            migrationBuilder.AddColumn<int>(
                name: "SparePartCategoryId",
                table: "SpareParts",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockDecrease",
                table: "StockDecrease",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SparePartCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePartCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderSparePart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    SparePartId = table.Column<int>(type: "int", nullable: false),
                    QuantityReceived = table.Column<int>(type: "int", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnitPriceAtPurchase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Margin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderSparePart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderSparePart_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderSparePart_SpareParts_SparePartId",
                        column: x => x.SparePartId,
                        principalTable: "SpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_SparePartCategoryId",
                table: "SpareParts",
                column: "SparePartCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSparePart_ProviderId",
                table: "ProviderSparePart",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSparePart_SparePartId",
                table: "ProviderSparePart",
                column: "SparePartId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_SparePartCategory_SparePartCategoryId",
                table: "SpareParts",
                column: "SparePartCategoryId",
                principalTable: "SparePartCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockDecrease_Interventions_InterventionId",
                table: "StockDecrease",
                column: "InterventionId",
                principalTable: "Interventions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockDecrease_SpareParts_SparePartId",
                table: "StockDecrease",
                column: "SparePartId",
                principalTable: "SpareParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_SparePartCategory_SparePartCategoryId",
                table: "SpareParts");

            migrationBuilder.DropForeignKey(
                name: "FK_StockDecrease_Interventions_InterventionId",
                table: "StockDecrease");

            migrationBuilder.DropForeignKey(
                name: "FK_StockDecrease_SpareParts_SparePartId",
                table: "StockDecrease");

            migrationBuilder.DropTable(
                name: "ProviderSparePart");

            migrationBuilder.DropTable(
                name: "SparePartCategory");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropIndex(
                name: "IX_SpareParts_SparePartCategoryId",
                table: "SpareParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockDecrease",
                table: "StockDecrease");

            migrationBuilder.DropColumn(
                name: "SparePartCategoryId",
                table: "SpareParts");

            migrationBuilder.RenameTable(
                name: "StockDecrease",
                newName: "InterventionSpareParts");

            migrationBuilder.RenameIndex(
                name: "IX_StockDecrease_SparePartId",
                table: "InterventionSpareParts",
                newName: "IX_InterventionSpareParts_SparePartId");

            migrationBuilder.RenameIndex(
                name: "IX_StockDecrease_InterventionId",
                table: "InterventionSpareParts",
                newName: "IX_InterventionSpareParts_InterventionId");

            migrationBuilder.RenameIndex(
                name: "IX_StockDecrease_DeliveryNote",
                table: "InterventionSpareParts",
                newName: "IX_InterventionSpareParts_DeliveryNote");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterventionSpareParts",
                table: "InterventionSpareParts",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_InterventionSpareParts_Interventions_InterventionId",
                table: "InterventionSpareParts",
                column: "InterventionId",
                principalTable: "Interventions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterventionSpareParts_SpareParts_SparePartId",
                table: "InterventionSpareParts",
                column: "SparePartId",
                principalTable: "SpareParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}