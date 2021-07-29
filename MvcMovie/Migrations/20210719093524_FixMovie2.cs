using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    public partial class FixMovie2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Movie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
