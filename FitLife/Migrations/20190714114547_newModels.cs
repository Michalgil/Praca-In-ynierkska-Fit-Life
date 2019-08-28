using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.Migrations
{
    public partial class newModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diets_AspNetUsers_ApplicationUserId",
                table: "Diets");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Diets_DietId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Meals_MealId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meals",
                table: "Meals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diets",
                table: "Diets");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Meals",
                newName: "Meal");

            migrationBuilder.RenameTable(
                name: "Diets",
                newName: "Diet");

            migrationBuilder.RenameIndex(
                name: "IX_Products_MealId",
                table: "Product",
                newName: "IX_Product_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_DietId",
                table: "Meal",
                newName: "IX_Meal_DietId");

            migrationBuilder.RenameIndex(
                name: "IX_Diets_ApplicationUserId",
                table: "Diet",
                newName: "IX_Diet_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meal",
                table: "Meal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diet",
                table: "Diet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diet_AspNetUsers_ApplicationUserId",
                table: "Diet",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_Diet_DietId",
                table: "Meal",
                column: "DietId",
                principalTable: "Diet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Meal_MealId",
                table: "Product",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diet_AspNetUsers_ApplicationUserId",
                table: "Diet");

            migrationBuilder.DropForeignKey(
                name: "FK_Meal_Diet_DietId",
                table: "Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Meal_MealId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meal",
                table: "Meal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diet",
                table: "Diet");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Meal",
                newName: "Meals");

            migrationBuilder.RenameTable(
                name: "Diet",
                newName: "Diets");

            migrationBuilder.RenameIndex(
                name: "IX_Product_MealId",
                table: "Products",
                newName: "IX_Products_MealId");

            migrationBuilder.RenameIndex(
                name: "IX_Meal_DietId",
                table: "Meals",
                newName: "IX_Meals_DietId");

            migrationBuilder.RenameIndex(
                name: "IX_Diet_ApplicationUserId",
                table: "Diets",
                newName: "IX_Diets_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meals",
                table: "Meals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diets",
                table: "Diets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diets_AspNetUsers_ApplicationUserId",
                table: "Diets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Diets_DietId",
                table: "Meals",
                column: "DietId",
                principalTable: "Diets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Meals_MealId",
                table: "Products",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
