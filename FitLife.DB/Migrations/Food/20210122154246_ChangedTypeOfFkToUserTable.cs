using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.DB.Migrations.Food
{
    public partial class ChangedTypeOfFkToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //IMPORTANT! HAD TO DELETE PK FROM THIS TABLE TO CREATE THIS MIGRATION
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeal_AppUser_UserId1",
                table: "UserMeal");

            migrationBuilder.DropIndex(
                name: "IX_UserMeal_UserId1",
                table: "UserMeal");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserMeal");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserMeal",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeal_AppUser_UserId",
                table: "UserMeal",
                column: "UserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeal_AppUser_UserId",
                table: "UserMeal");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserMeal",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserMeal",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMeal_UserId1",
                table: "UserMeal",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeal_AppUser_UserId1",
                table: "UserMeal",
                column: "UserId1",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
