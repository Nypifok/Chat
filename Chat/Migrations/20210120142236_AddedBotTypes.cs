using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Migrations
{
    public partial class AddedBotTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatBot_Bot_BotId",
                table: "ChatBot");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatBot_Chats_ChatId",
                table: "ChatBot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatBot",
                table: "ChatBot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bot",
                table: "Bot");

            migrationBuilder.RenameTable(
                name: "ChatBot",
                newName: "ChatBots");

            migrationBuilder.RenameTable(
                name: "Bot",
                newName: "Bots");

            migrationBuilder.RenameIndex(
                name: "IX_ChatBot_ChatId",
                table: "ChatBots",
                newName: "IX_ChatBots_ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_Bot_Title",
                table: "Bots",
                newName: "IX_Bots_Title");

            migrationBuilder.AddColumn<Guid>(
                name: "BotId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatBots",
                table: "ChatBots",
                columns: new[] { "BotId", "ChatId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bots",
                table: "Bots",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TypesOfBots",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfBots", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "BotTypes",
                columns: table => new
                {
                    BotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotTypes", x => new { x.BotId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_BotTypes_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BotTypes_TypesOfBots_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TypesOfBots",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_BotId",
                table: "Messages",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_BotTypes_TypeId",
                table: "BotTypes",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatBots_Bots_BotId",
                table: "ChatBots",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatBots_Chats_ChatId",
                table: "ChatBots",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Bots_BotId",
                table: "Messages",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatBots_Bots_BotId",
                table: "ChatBots");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatBots_Chats_ChatId",
                table: "ChatBots");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Bots_BotId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "BotTypes");

            migrationBuilder.DropTable(
                name: "TypesOfBots");

            migrationBuilder.DropIndex(
                name: "IX_Messages_BotId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatBots",
                table: "ChatBots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bots",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "BotId",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "ChatBots",
                newName: "ChatBot");

            migrationBuilder.RenameTable(
                name: "Bots",
                newName: "Bot");

            migrationBuilder.RenameIndex(
                name: "IX_ChatBots_ChatId",
                table: "ChatBot",
                newName: "IX_ChatBot_ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_Bots_Title",
                table: "Bot",
                newName: "IX_Bot_Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatBot",
                table: "ChatBot",
                columns: new[] { "BotId", "ChatId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bot",
                table: "Bot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatBot_Bot_BotId",
                table: "ChatBot",
                column: "BotId",
                principalTable: "Bot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatBot_Chats_ChatId",
                table: "ChatBot",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
