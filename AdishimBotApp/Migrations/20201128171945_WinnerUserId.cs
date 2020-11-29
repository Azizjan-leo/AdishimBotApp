using Microsoft.EntityFrameworkCore.Migrations;

namespace AdishimBotApp.Migrations
{
    public partial class WinnerUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WinnerUsername",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "WinnerUserId",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WinnerUserId",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "WinnerUsername",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
