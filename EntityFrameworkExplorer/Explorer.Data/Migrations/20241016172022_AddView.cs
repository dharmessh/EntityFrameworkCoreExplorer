using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Explorer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    CREATE VIEW view_team_league
                    AS
                    SELECT t.Name, l.Name AS LeagueName
                    FROM Teams AS t
                    LEFT JOIN Leagues AS l ON t.LeagueId = l.Id;
                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW view_team_league");
        }
    }
}
