using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.Migrations
{
    public partial class AddCustomUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isMale",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isMale",
                table: "AspNetUsers");
        }
    }
}
