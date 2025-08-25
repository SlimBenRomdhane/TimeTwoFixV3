using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Data;

#nullable disable

namespace TimeTwoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateInterventionTimeSpanToBigInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
       name: "ActualTimeSpentTicks",
       table: "Interventions",
       nullable: true
    );
            // Step 2: Convert existing time values to ticks
            migrationBuilder.Sql(@"
        UPDATE Interventions
        SET ActualTimeSpentTicks = DATEDIFF_BIG(NANOSECOND, 0, ActualTimeSpent) / 100
        WHERE ActualTimeSpent IS NOT NULL
    ");

            migrationBuilder.Sql(@"
        DECLARE @var0 sysname;
        SELECT @var0 = [d].[name]
        FROM [sys].[default_constraints] [d]
        INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
        WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Interventions]') AND [c].[name] = N'ActualTimeSpent');
        IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Interventions] DROP CONSTRAINT [' + @var0 + ']');
    ");


            // Step 4: Drop old column
            migrationBuilder.DropColumn(
                name: "ActualTimeSpent",
                table: "Interventions"
            );

            // Step 5: Rename temp column to ActualTimeSpent
            migrationBuilder.RenameColumn(
                name: "ActualTimeSpentTicks",
                table: "Interventions",
                newName: "ActualTimeSpent"
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ActualTimeSpent",
                table: "Interventions",
                type: "time",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { "a141f8f9-9dc8-4bf6-a9b4-a28830da60b2", new DateTime(2025, 8, 19, 19, 52, 33, 62, DateTimeKind.Utc).AddTicks(654), "AQAAAAIAAYagAAAAEK001lQ0KEX/qq/LhapVxLPkIoUaOZ0DeXAnvYGf3mAJ9HiobPtZMwVonS8crIzR5g==", new DateTime(2025, 8, 19, 19, 52, 33, 62, DateTimeKind.Utc).AddTicks(655) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 19, 19, 52, 33, 62, DateTimeKind.Utc).AddTicks(133));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 19, 19, 52, 33, 62, DateTimeKind.Utc).AddTicks(138));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 19, 19, 52, 33, 62, DateTimeKind.Utc).AddTicks(139));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 19, 19, 52, 33, 62, DateTimeKind.Utc).AddTicks(140));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 8, 19, 19, 52, 33, 62, DateTimeKind.Utc).AddTicks(141));
        }
    }
}
