using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Infrastructure.Identity.Migrations
{
    public partial class SimplifyApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "IdentityUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "IdentityUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "IdentityUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
