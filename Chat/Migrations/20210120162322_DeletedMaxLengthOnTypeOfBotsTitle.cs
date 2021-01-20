using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Migrations
{
    public partial class DeletedMaxLengthOnTypeOfBotsTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotTypes_TypesOfBots_TypeId",
                table: "BotTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotTypes",
                table: "BotTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TypesOfBots",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "TypeId",
                table: "BotTypes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BotTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotTypes",
                table: "BotTypes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BotTypes_BotId",
                table: "BotTypes",
                column: "BotId");

            migrationBuilder.AddForeignKey(
                name: "FK_BotTypes_TypesOfBots_TypeId",
                table: "BotTypes",
                column: "TypeId",
                principalTable: "TypesOfBots",
                principalColumn: "Title",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotTypes_TypesOfBots_TypeId",
                table: "BotTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotTypes",
                table: "BotTypes");

            migrationBuilder.DropIndex(
                name: "IX_BotTypes_BotId",
                table: "BotTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BotTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TypesOfBots",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TypeId",
                table: "BotTypes",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotTypes",
                table: "BotTypes",
                columns: new[] { "BotId", "TypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BotTypes_TypesOfBots_TypeId",
                table: "BotTypes",
                column: "TypeId",
                principalTable: "TypesOfBots",
                principalColumn: "Title",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
