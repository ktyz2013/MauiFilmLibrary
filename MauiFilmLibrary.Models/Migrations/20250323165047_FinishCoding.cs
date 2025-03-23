using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiFilmLibrary.Models.Migrations
{
    /// <inheritdoc />
    public partial class FinishCoding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gener_movie");

            migrationBuilder.DropTable(
                name: "gener");

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    genre_name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "genre_movie",
                columns: table => new
                {
                    gener_id = table.Column<int>(type: "INTEGER", nullable: false),
                    movie_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre_movie", x => new { x.gener_id, x.movie_id });
                    table.ForeignKey(
                        name: "FK_genre_movie_genre_gener_id",
                        column: x => x.gener_id,
                        principalTable: "genre",
                        principalColumn: "genre_id");
                    table.ForeignKey(
                        name: "FK_genre_movie_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_genre_genre_name",
                table: "genre",
                column: "genre_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_genre_movie_movie_id",
                table: "genre_movie",
                column: "movie_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "genre_movie");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.CreateTable(
                name: "gener",
                columns: table => new
                {
                    gener_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    gener_name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gener", x => x.gener_id);
                });

            migrationBuilder.CreateTable(
                name: "gener_movie",
                columns: table => new
                {
                    gener_id = table.Column<int>(type: "INTEGER", nullable: false),
                    movie_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gener_movie", x => new { x.gener_id, x.movie_id });
                    table.ForeignKey(
                        name: "FK_gener_movie_gener_gener_id",
                        column: x => x.gener_id,
                        principalTable: "gener",
                        principalColumn: "gener_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gener_movie_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gener_gener_name",
                table: "gener",
                column: "gener_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gener_movie_movie_id",
                table: "gener_movie",
                column: "movie_id");
        }
    }
}
