using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_ActorId",
                table: "Movie",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Actor_ActorId",
                table: "Movie",
                column: "ActorId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Actor_ActorId",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Movie_ActorId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Movie");
        }
    }
}
