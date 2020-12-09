using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.DB.Migrations.Food
{
    public partial class MealNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Meal",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Meal");
        }
    }
}
