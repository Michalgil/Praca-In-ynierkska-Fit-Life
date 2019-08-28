using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.Migrations
{
    public partial class addNewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Meal_MealId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_MealId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "PartOfBody",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfBody", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Break = table.Column<int>(nullable: false),
                    isBegginer = table.Column<bool>(nullable: false),
                    isIntermediate = table.Column<bool>(nullable: false),
                    isAdvanced = table.Column<bool>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NumberOfSeries = table.Column<int>(nullable: false),
                    NumberOfRepetitions = table.Column<int>(nullable: false),
                    PartOfBodyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_PartOfBody_PartOfBodyId",
                        column: x => x.PartOfBodyId,
                        principalTable: "PartOfBody",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingExercise",
                columns: table => new
                {
                    TrainingId = table.Column<int>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingExercise", x => new { x.TrainingId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_TrainingExercise_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingExercise_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_PartOfBodyId",
                table: "Exercise",
                column: "PartOfBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_Training_ApplicationUserId",
                table: "Training",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercise_ExerciseId",
                table: "TrainingExercise",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingExercise");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "PartOfBody");

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_MealId",
                table: "Product",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Meal_MealId",
                table: "Product",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
