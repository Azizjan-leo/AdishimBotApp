using Microsoft.EntityFrameworkCore.Migrations;

namespace AdishimBotApp.Migrations
{
    public partial class CrocoStarterId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StarterUserId",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarterUserId",
                table: "Games");
        }
    }
}
