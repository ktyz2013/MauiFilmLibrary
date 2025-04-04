﻿// <auto-generated />
using System;
using MauiFilmLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MauiFilmLibrary.Models.Migrations
{
    [DbContext(typeof(MovieDatabaseContext))]
    partial class MovieDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.14");

            modelBuilder.Entity("MauiFilmLibrary.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("genre_id");

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("genre_name");

                    b.HasKey("GenreId");

                    b.HasIndex(new[] { "GenreName" }, "IX_genre_genre_name")
                        .IsUnique();

                    b.ToTable("genre", (string)null);
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.GenreMovie", b =>
                {
                    b.Property<int>("GenreId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("gener_id");

                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("movie_id");

                    b.HasKey("GenreId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("genre_movie", (string)null);
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("movie_id");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<int?>("GenreId")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly?>("ReleaseYear")
                        .HasColumnType("TEXT")
                        .HasColumnName("release_year");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.Property<byte[]>("TitleImg")
                        .HasColumnType("BLOB")
                        .HasColumnName("title_img");

                    b.HasKey("MovieId");

                    b.HasIndex("GenreId");

                    b.ToTable("movie", (string)null);
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.MoviePersonRole", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("movie_id");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("person_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("role_id");

                    b.HasKey("MovieId", "PersonId", "RoleId");

                    b.HasIndex("PersonId");

                    b.HasIndex("RoleId");

                    b.ToTable("movie_person_role", (string)null);
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("person_id");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("person_name");

                    b.HasKey("PersonId");

                    b.ToTable("person", (string)null);
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("role_id");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("role_name");

                    b.HasKey("RoleId");

                    b.HasIndex(new[] { "RoleName" }, "IX_role_role_name")
                        .IsUnique();

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.GenreMovie", b =>
                {
                    b.HasOne("MauiFilmLibrary.Models.Genre", "Genre")
                        .WithMany("GenreMovies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MauiFilmLibrary.Models.Movie", "Movie")
                        .WithMany("GenreMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.Movie", b =>
                {
                    b.HasOne("MauiFilmLibrary.Models.Genre", null)
                        .WithMany("Movies")
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.MoviePersonRole", b =>
                {
                    b.HasOne("MauiFilmLibrary.Models.Movie", "Movie")
                        .WithMany("MoviePersonRoles")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MauiFilmLibrary.Models.Person", "Person")
                        .WithMany("MoviePersonRoles")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MauiFilmLibrary.Models.Role", "Role")
                        .WithMany("MoviePersonRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Person");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.Genre", b =>
                {
                    b.Navigation("GenreMovies");

                    b.Navigation("Movies");
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.Movie", b =>
                {
                    b.Navigation("GenreMovies");

                    b.Navigation("MoviePersonRoles");
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.Person", b =>
                {
                    b.Navigation("MoviePersonRoles");
                });

            modelBuilder.Entity("MauiFilmLibrary.Models.Role", b =>
                {
                    b.Navigation("MoviePersonRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
