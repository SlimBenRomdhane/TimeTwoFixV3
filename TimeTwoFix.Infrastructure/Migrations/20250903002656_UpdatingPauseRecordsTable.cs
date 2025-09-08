using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingPauseRecordsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PauseRecord_Interventions_InterventionId",
                table: "PauseRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PauseRecord",
                table: "PauseRecord");

            migrationBuilder.RenameTable(
                name: "PauseRecord",
                newName: "PauseRecords");

            migrationBuilder.RenameIndex(
                name: "IX_PauseRecord_InterventionId",
                table: "PauseRecords",
                newName: "IX_PauseRecords_InterventionId");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PauseDuration",
                table: "PauseRecords",
                type: "time",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PauseRecords",
                table: "PauseRecords",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "548c2be5-f095-4c30-b06f-db58be95214f", new DateTime(2025, 9, 3, 0, 26, 54, 320, DateTimeKind.Utc).AddTicks(2093), "AQAAAAIAAYagAAAAEGGzw7EFvjeugxVOTFN/QXx+6u+ENVLrvvmRXIBOR+QqEeaxFRw+WmsZdTSFXAw7Eg==", new DateTime(2025, 9, 3, 0, 26, 54, 320, DateTimeKind.Utc).AddTicks(2093) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 3, 0, 26, 54, 320, DateTimeKind.Utc).AddTicks(1820));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 3, 0, 26, 54, 320, DateTimeKind.Utc).AddTicks(1829));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 3, 0, 26, 54, 320, DateTimeKind.Utc).AddTicks(1831));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 3, 0, 26, 54, 320, DateTimeKind.Utc).AddTicks(1833));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 3, 0, 26, 54, 320, DateTimeKind.Utc).AddTicks(1834));

            migrationBuilder.AddForeignKey(
                name: "FK_PauseRecords_Interventions_InterventionId",
                table: "PauseRecords",
                column: "InterventionId",
                principalTable: "Interventions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PauseRecords_Interventions_InterventionId",
                table: "PauseRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PauseRecords",
                table: "PauseRecords");

            migrationBuilder.DropColumn(
                name: "PauseDuration",
                table: "PauseRecords");

            migrationBuilder.RenameTable(
                name: "PauseRecords",
                newName: "PauseRecord");

            migrationBuilder.RenameIndex(
                name: "IX_PauseRecords_InterventionId",
                table: "PauseRecord",
                newName: "IX_PauseRecord_InterventionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PauseRecord",
                table: "PauseRecord",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "16b6d2bd-42e2-4dbd-b715-b0da0b22f277", new DateTime(2025, 8, 31, 20, 42, 16, 274, DateTimeKind.Utc).AddTicks(3670), "AQAAAAIAAYagAAAAEJVCdTL9Z/dDLRwMA+M7LB6Qj1dsxEDj4Wlf+aUZDisxI8lipDV1CdsGuWlON414Sg==", new DateTime(2025, 8, 31, 20, 42, 16, 274, DateTimeKind.Utc).AddTicks(3671) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 31, 20, 42, 16, 274, DateTimeKind.Utc).AddTicks(3426));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 31, 20, 42, 16, 274, DateTimeKind.Utc).AddTicks(3434));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 31, 20, 42, 16, 274, DateTimeKind.Utc).AddTicks(3435));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 31, 20, 42, 16, 274, DateTimeKind.Utc).AddTicks(3437));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 31, 20, 42, 16, 274, DateTimeKind.Utc).AddTicks(3437));

            migrationBuilder.AddForeignKey(
                name: "FK_PauseRecord_Interventions_InterventionId",
                table: "PauseRecord",
                column: "InterventionId",
                principalTable: "Interventions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}