using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiFilmLibrary.Models.Migrations
{
    /// <inheritdoc />
    public partial class FixGenreMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_genre_movie_genre_gener_id",
                table: "genre_movie");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "movie",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_movie_GenreId",
                table: "movie",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_genre_movie_genre_gener_id",
                table: "genre_movie",
                column: "gener_id",
                principalTable: "genre",
                principalColumn: "genre_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_movie_genre_GenreId",
                table: "movie",
                column: "GenreId",
                principalTable: "genre",
                principalColumn: "genre_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_genre_movie_genre_gener_id",
                table: "genre_movie");

            migrationBuilder.DropForeignKey(
                name: "FK_movie_genre_GenreId",
                table: "movie");

            migrationBuilder.DropIndex(
                name: "IX_movie_GenreId",
                table: "movie");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "movie");

            migrationBuilder.AddForeignKey(
                name: "FK_genre_movie_genre_gener_id",
                table: "genre_movie",
                column: "gener_id",
                principalTable: "genre",
                principalColumn: "genre_id");
        }
    }
}
