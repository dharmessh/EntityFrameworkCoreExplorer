﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Explorer.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "CreationDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 9, 13, 36, 27, 770, DateTimeKind.Local).AddTicks(7688), "India" },
                    { 2, new DateTime(2024, 10, 9, 13, 36, 27, 770, DateTimeKind.Local).AddTicks(7719), "Bangladesh" },
                    { 3, new DateTime(2024, 10, 9, 13, 36, 27, 770, DateTimeKind.Local).AddTicks(7720), "Russia" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 3);
        }
    }
}
