using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.DB.Migrations.Food
{
    public partial class ChangedDecimalColumnsTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProteinsGrams",
                table: "Product",
                type: "decimal(4,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FatsGrams",
                table: "Product",
                type: "decimal(4,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CarbsGrams",
                table: "Product",
                type: "decimal(4,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Calories",
                table: "Product",
                type: "decimal(4,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Calories",
                table: "Meal",
                type: "decimal(4,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProteinsGrams",
                table: "Product",
                type: "decimal(3,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FatsGrams",
                table: "Product",
                type: "decimal(3,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CarbsGrams",
                table: "Product",
                type: "decimal(3,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Calories",
                table: "Product",
                type: "decimal(3,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Calories",
                table: "Meal",
                type: "decimal(3,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)");
        }
    }
}
