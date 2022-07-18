using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCRUD.Migrations
{
    public partial class Update_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
