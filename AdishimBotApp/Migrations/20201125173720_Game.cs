using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdishimBotApp.Migrations
{
    public partial class Game : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<long>(nullable: false),
                    StartUtc = table.Column<DateTime>(nullable: false),
                    EndUtc = table.Column<DateTime>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    RightAnswer = table.Column<string>(nullable: true),
                    WinnerUsername = table.Column<string>(nullable: true),
                    Closed = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
