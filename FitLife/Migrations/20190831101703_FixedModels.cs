using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.Migrations
{
    public partial class FixedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Training",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Diet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Diet");
        }
    }
}
