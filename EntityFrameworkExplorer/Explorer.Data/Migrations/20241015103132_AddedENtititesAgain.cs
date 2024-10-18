using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Explorer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedENtititesAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Teams");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 10, 15, 10, 31, 31, 848, DateTimeKind.Local).AddTicks(3963));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 10, 15, 10, 31, 31, 848, DateTimeKind.Local).AddTicks(3984));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 10, 15, 10, 31, 31, 848, DateTimeKind.Local).AddTicks(3985));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Teams",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "TeamId" },
                values: new object[] { new DateTime(2024, 10, 15, 10, 26, 49, 869, DateTimeKind.Local).AddTicks(2220), null });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "TeamId" },
                values: new object[] { new DateTime(2024, 10, 15, 10, 26, 49, 869, DateTimeKind.Local).AddTicks(2244), null });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "TeamId" },
                values: new object[] { new DateTime(2024, 10, 15, 10, 26, 49, 869, DateTimeKind.Local).AddTicks(2245), null });
        }
    }
}
