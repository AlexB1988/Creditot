using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Creditot.Migrations
{
    public partial class ChatIdInCredits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Credits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Credits");
        }
    }
}
