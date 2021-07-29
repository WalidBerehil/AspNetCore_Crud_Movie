using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    public partial class FixCategoryMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryMovie_Category_categoriesId",
                table: "CategoryMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryMovie_Movie_moviesId",
                table: "CategoryMovie");

            migrationBuilder.RenameColumn(
                name: "moviesId",
                table: "CategoryMovie",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "categoriesId",
                table: "CategoryMovie",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryMovie_moviesId",
                table: "CategoryMovie",
                newName: "IX_CategoryMovie_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryMovie_Category_CategoryId",
                table: "CategoryMovie",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryMovie_Movie_MovieId",
                table: "CategoryMovie",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryMovie_Category_CategoryId",
                table: "CategoryMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryMovie_Movie_MovieId",
                table: "CategoryMovie");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "CategoryMovie",
                newName: "moviesId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "CategoryMovie",
                newName: "categoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryMovie_MovieId",
                table: "CategoryMovie",
                newName: "IX_CategoryMovie_moviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryMovie_Category_categoriesId",
                table: "CategoryMovie",
                column: "categoriesId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryMovie_Movie_moviesId",
                table: "CategoryMovie",
                column: "moviesId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
