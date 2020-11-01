using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Infrastructure.Data.Migrations
{
    public partial class AddNamesToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorFirst",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorLast",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorFirst",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorLast",
                table: "Posts");
        }
    }
}
