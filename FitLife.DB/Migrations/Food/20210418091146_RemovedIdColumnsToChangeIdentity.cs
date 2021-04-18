using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.DB.Migrations.Food
{
    public partial class RemovedIdColumnsToChangeIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealProduct",
                table: "MealProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserMeal");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MealProduct");


            migrationBuilder.AddColumn<int>(
                    name: "UserMealId",
                    table: "UserMeal",
                    nullable: false,
                    defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                    name: "MealProductId",
                    table: "MealProduct",
                    nullable: false,
                    defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMeal",
                table: "UserMeal",
                column: "UserMealId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealProduct",
                table: "MealProduct",
                column: "MealProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeal_AppUser_UserId",
                table: "UserMeal");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMeal",
                table: "UserMeal");

            migrationBuilder.DropIndex(
                name: "IX_UserMeal_UserId",
                table: "UserMeal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealProduct",
                table: "MealProduct");

            migrationBuilder.DropIndex(
                name: "IX_MealProduct_MealId",
                table: "MealProduct");

            migrationBuilder.DropColumn(
                name: "UserMealId",
                table: "UserMeal");

            migrationBuilder.DropColumn(
                name: "MealProductId",
                table: "MealProduct");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserMeal",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserMeal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MealProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMeal",
                table: "UserMeal",
                columns: new[] { "UserId", "MealId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealProduct",
                table: "MealProduct",
                columns: new[] { "MealId", "ProductId" });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeal_IdentityUser_UserId",
                table: "UserMeal",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
