using Microsoft.EntityFrameworkCore.Migrations;

namespace AdishimBotApp.Migrations
{
    public partial class IlineWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Words_WordId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_WordId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "WordId",
                table: "Words");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Words",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RuText",
                table: "Words",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrText",
                table: "Words",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "RuText",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "UrText",
                table: "Words");

            migrationBuilder.AddColumn<byte>(
                name: "Language",
                table: "Words",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WordId",
                table: "Words",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_WordId",
                table: "Words",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Words_WordId",
                table: "Words",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
