using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiFilmLibrary.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddGenerMovieRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "movie",
                columns: table => new
                {
                    movie_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    release_year = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    title_img = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.movie_id);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    person_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    person_name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.person_id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    role_name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
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

            migrationBuilder.CreateTable(
                name: "movie_person_role",
                columns: table => new
                {
                    movie_id = table.Column<int>(type: "INTEGER", nullable: false),
                    person_id = table.Column<int>(type: "INTEGER", nullable: false),
                    role_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_person_role", x => new { x.movie_id, x.person_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_movie_person_role_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_person_role_person_person_id",
                        column: x => x.person_id,
                        principalTable: "person",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_person_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "role_id",
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

            migrationBuilder.CreateIndex(
                name: "IX_movie_person_role_person_id",
                table: "movie_person_role",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_person_role_role_id",
                table: "movie_person_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_role_name",
                table: "role",
                column: "role_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gener_movie");

            migrationBuilder.DropTable(
                name: "movie_person_role");

            migrationBuilder.DropTable(
                name: "gener");

            migrationBuilder.DropTable(
                name: "movie");

            migrationBuilder.DropTable(
                name: "person");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
