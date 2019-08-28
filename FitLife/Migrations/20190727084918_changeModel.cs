using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.Migrations
{
    public partial class changeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRepetitions",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "NumberOfSeries",
                table: "Exercise");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfRepetitions",
                table: "Exercise",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeries",
                table: "Exercise",
                nullable: false,
                defaultValue: 0);
        }
    }
}
