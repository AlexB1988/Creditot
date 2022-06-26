using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Creditot.Migrations
{
    public partial class AddIsDeletedColomn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Credits",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Credits");
        }
    }
}
