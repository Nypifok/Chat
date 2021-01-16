using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Migrations
{
    public partial class AddedBots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatBot",
                columns: table => new
                {
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBot", x => new { x.BotId, x.ChatId });
                    table.ForeignKey(
                        name: "FK_ChatBot_Bot_BotId",
                        column: x => x.BotId,
                        principalTable: "Bot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatBot_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bot_Title",
                table: "Bot",
                column: "Title",
                unique: true,
                filter: "[Title] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChatBot_ChatId",
                table: "ChatBot",
                column: "ChatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatBot");

            migrationBuilder.DropTable(
                name: "Bot");
        }
    }
}
