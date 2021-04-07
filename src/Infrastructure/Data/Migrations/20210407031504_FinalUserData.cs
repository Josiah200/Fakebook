using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Infrastructure.Data.Migrations
{
    public partial class FinalUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Workplace",
                table: "Users",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Hometown",
                table: "Users",
                newName: "JobTitle");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HometownCity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HometownState",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HometownCity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HometownState",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Users",
                newName: "Workplace");

            migrationBuilder.RenameColumn(
                name: "JobTitle",
                table: "Users",
                newName: "Hometown");
        }
    }
}
