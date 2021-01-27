using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitLife.DB.Migrations
{
    public partial class ChangedAuthenticationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "UserMeal");

            //migrationBuilder.DropColumn(
            //    name: "Discriminator",
            //    table: "AspNetUsers");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "UserMeal",
            //    nullable: true,
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier");

            //migrationBuilder.AddColumn<int>(
            //    name: "CategoryId",
            //    table: "UserMeal",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "ConsumedDate",
            //    table: "UserMeal",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserMeal_CategoryId",
            //    table: "UserMeal",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserMeal_UserId",
            //    table: "UserMeal",
            //    column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserMeal_Category_CategoryId",
            //    table: "UserMeal",
            //    column: "CategoryId",
            //    principalTable: "Category",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeal_AspNetUsers_UserId",
                table: "UserMeal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMeal_Category_CategoryId",
                table: "UserMeal");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMeal_AspNetUsers_UserId",
                table: "UserMeal");

            migrationBuilder.DropIndex(
                name: "IX_UserMeal_CategoryId",
                table: "UserMeal");

            migrationBuilder.DropIndex(
                name: "IX_UserMeal_UserId",
                table: "UserMeal");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "UserMeal");

            migrationBuilder.DropColumn(
                name: "ConsumedDate",
                table: "UserMeal");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserMeal",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserMeal",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserMeal_UserId1",
                table: "UserMeal",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMeal_AspNetUsers_UserId1",
                table: "UserMeal",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
