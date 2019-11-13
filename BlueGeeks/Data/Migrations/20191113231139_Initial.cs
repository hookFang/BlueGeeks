using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlueGeeks.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Team_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team_Name = table.Column<string>(nullable: false),
                    Team_Mascot = table.Column<string>(nullable: false),
                    Conference = table.Column<string>(nullable: false),
                    Wins = table.Column<short>(nullable: false),
                    Loses = table.Column<short>(nullable: false),
                    Ties = table.Column<short>(nullable: false),
                    Win_Streak = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Team_Id);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Coaches_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Team_Id = table.Column<int>(nullable: false),
                    Team_Id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Coaches_Id);
                    table.ForeignKey(
                        name: "FK_Coaches_Teams_Team_Id1",
                        column: x => x.Team_Id1,
                        principalTable: "Teams",
                        principalColumn: "Team_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Player_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    JerseyNumber = table.Column<short>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    PlayerTeamTeam_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Player_Id);
                    table.ForeignKey(
                        name: "FK_Player_Teams_PlayerTeamTeam_Id",
                        column: x => x.PlayerTeamTeam_Id,
                        principalTable: "Teams",
                        principalColumn: "Team_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stadium",
                columns: table => new
                {
                    Stadium_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StadiumName = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Team_Id = table.Column<int>(nullable: false),
                    Team_Id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadium", x => x.Stadium_Id);
                    table.ForeignKey(
                        name: "FK_Stadium_Teams_Team_Id1",
                        column: x => x.Team_Id1,
                        principalTable: "Teams",
                        principalColumn: "Team_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatistics",
                columns: table => new
                {
                    Player_Statistics_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FgPercent = table.Column<float>(nullable: false),
                    FtPercent = table.Column<float>(nullable: false),
                    ThreePointersMade = table.Column<short>(nullable: false),
                    PointsMade = table.Column<short>(nullable: false),
                    Rebounds = table.Column<short>(nullable: false),
                    Assists = table.Column<short>(nullable: false),
                    Steals = table.Column<short>(nullable: false),
                    Blocks = table.Column<short>(nullable: false),
                    TurnOvers = table.Column<short>(nullable: false),
                    Player_Id = table.Column<int>(nullable: false),
                    PlayerIdPlayer_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistics", x => x.Player_Statistics_Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Player_PlayerIdPlayer_Id",
                        column: x => x.PlayerIdPlayer_Id,
                        principalTable: "Player",
                        principalColumn: "Player_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Matche_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeam = table.Column<string>(nullable: false),
                    AwayTeam = table.Column<string>(nullable: false),
                    MatchDate = table.Column<DateTime>(nullable: false),
                    AwayTeam_Id = table.Column<int>(nullable: false),
                    TeamAwayTeam_Id = table.Column<int>(nullable: true),
                    Stadium_Id = table.Column<int>(nullable: false),
                    TeamHomeStadium_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Matche_Id);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamAwayTeam_Id",
                        column: x => x.TeamAwayTeam_Id,
                        principalTable: "Teams",
                        principalColumn: "Team_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Stadium_TeamHomeStadium_Id",
                        column: x => x.TeamHomeStadium_Id,
                        principalTable: "Stadium",
                        principalColumn: "Stadium_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_Team_Id1",
                table: "Coaches",
                column: "Team_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamAwayTeam_Id",
                table: "Matches",
                column: "TeamAwayTeam_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamHomeStadium_Id",
                table: "Matches",
                column: "TeamHomeStadium_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Player_PlayerTeamTeam_Id",
                table: "Player",
                column: "PlayerTeamTeam_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerIdPlayer_Id",
                table: "PlayerStatistics",
                column: "PlayerIdPlayer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stadium_Team_Id1",
                table: "Stadium",
                column: "Team_Id1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "PlayerStatistics");

            migrationBuilder.DropTable(
                name: "Stadium");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
