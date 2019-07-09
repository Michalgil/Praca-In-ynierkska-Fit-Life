using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.Migrations
{
    public partial class FixProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "ProductCategory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "ProductCategory");
        }
    }
}
