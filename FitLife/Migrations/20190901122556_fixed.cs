using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.Migrations
{
    public partial class @fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isIntermediate",
                table: "Training",
                newName: "IsIntermediate");

            migrationBuilder.RenameColumn(
                name: "isBegginer",
                table: "Training",
                newName: "IsBegginer");

            migrationBuilder.RenameColumn(
                name: "isAdvanced",
                table: "Training",
                newName: "IsAdvanced");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Training",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PriorityPart",
                table: "Training",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "PriorityPart",
                table: "Training");

            migrationBuilder.RenameColumn(
                name: "IsIntermediate",
                table: "Training",
                newName: "isIntermediate");

            migrationBuilder.RenameColumn(
                name: "IsBegginer",
                table: "Training",
                newName: "isBegginer");

            migrationBuilder.RenameColumn(
                name: "IsAdvanced",
                table: "Training",
                newName: "isAdvanced");
        }
    }
}
