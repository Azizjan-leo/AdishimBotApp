using Microsoft.EntityFrameworkCore.Migrations;

namespace AdishimBotApp.Migrations
{
    public partial class WordIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrText",
                table: "Words",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RuText",
                table: "Words",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_RuText_UrText",
                table: "Words",
                columns: new[] { "RuText", "UrText" },
                unique: true,
                filter: "[RuText] IS NOT NULL AND [UrText] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Words_RuText_UrText",
                table: "Words");

            migrationBuilder.AlterColumn<string>(
                name: "UrText",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RuText",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
