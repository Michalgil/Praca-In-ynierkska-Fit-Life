using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.Migrations
{
    public partial class EditDietModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Mass",
                table: "Diet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WeightMaintenance",
                table: "Diet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WeightReduction",
                table: "Diet",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mass",
                table: "Diet");

            migrationBuilder.DropColumn(
                name: "WeightMaintenance",
                table: "Diet");

            migrationBuilder.DropColumn(
                name: "WeightReduction",
                table: "Diet");
        }
    }
}
