using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Infrastructure.Data.Migrations
{
    public partial class HomeCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HometownCountry",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HometownCountry",
                table: "Users");
        }
    }
}
