using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.DB.Migrations.Food
{
    public partial class CategpryIdUserMealCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "UserMeal",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserMeal_CategoryId",
                table: "UserMeal",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeal_Category_CategoryId",
                table: "UserMeal",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeal_Category_CategoryId",
                table: "UserMeal");

            migrationBuilder.DropIndex(
                name: "IX_UserMeal_CategoryId",
                table: "UserMeal");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "UserMeal");
        }
    }
}
