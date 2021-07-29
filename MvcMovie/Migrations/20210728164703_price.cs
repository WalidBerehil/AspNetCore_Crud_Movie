using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    public partial class price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Movie");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Movie",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Movie",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
