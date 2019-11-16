using Microsoft.EntityFrameworkCore.Migrations;

namespace BlueGeeks.Data.Migrations
{
    public partial class MatchesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_TeamAwayTeam_Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_TeamAwayTeam_Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "AwayTeam",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "HomeTeam",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamAwayTeam_Id",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamTeam_Id",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamTeam_Id",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeam_Id",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamTeam_Id",
                table: "Matches",
                column: "AwayTeamTeam_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamTeam_Id",
                table: "Matches",
                column: "HomeTeamTeam_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_AwayTeamTeam_Id",
                table: "Matches",
                column: "AwayTeamTeam_Id",
                principalTable: "Teams",
                principalColumn: "Team_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_HomeTeamTeam_Id",
                table: "Matches",
                column: "HomeTeamTeam_Id",
                principalTable: "Teams",
                principalColumn: "Team_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_AwayTeamTeam_Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_HomeTeamTeam_Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_AwayTeamTeam_Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_HomeTeamTeam_Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "AwayTeamTeam_Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "HomeTeamTeam_Id",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "HomeTeam_Id",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "AwayTeam",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeTeam",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TeamAwayTeam_Id",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamAwayTeam_Id",
                table: "Matches",
                column: "TeamAwayTeam_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_TeamAwayTeam_Id",
                table: "Matches",
                column: "TeamAwayTeam_Id",
                principalTable: "Teams",
                principalColumn: "Team_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
