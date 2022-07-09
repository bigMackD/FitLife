using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitLife.DB.Migrations.Diet
{
    public partial class UserPeriodicDietsTableGenerated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPeriodicDiet",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Calories = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    ProteinsGrams = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    CarbsGrams = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    FatsGrams = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "date", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPeriodicDiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPeriodicDiet_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPeriodicDiet");
        }
    }
}
