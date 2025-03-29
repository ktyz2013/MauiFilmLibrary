using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MauiFilmLibrary.Models;

public partial class MovieDatabaseContext : DbContext
{
    #region CONSTRUCTOR

    /// <summary>
    /// Constructor for creating migrations
    /// </summary>
    public MovieDatabaseContext()
    {
        File = Path.Combine("../", "UsedByMigratorOnly1.db3");
        Initialize();
    }

    ///<summary>
    ///Constructor for mobile app
    ///</summary>
    /// <param name="filenameWithPath"></param>
    public MovieDatabaseContext(string filenameWithPath)
    {
        File = filenameWithPath;
        Initialize();
    }
    void Initialize()
    {
        if (!Initialized)
        {
            Initialized = true;

            SQLitePCL.Batteries_V2.Init();

            //Database.EnsureDeleted(); //use in dev process when needed

            //Database.Migrate();
        }
    }
    public static async Task<MovieDatabaseContext> CreateAsync(string filenameWithPath)
    {
        var instance = new MovieDatabaseContext(filenameWithPath);
        await instance.InitializeAsync();
        return instance;
    }
    private async Task InitializeAsync()
    {
        if (!Initialized)
        {
            Initialized = true;

            SQLitePCL.Batteries_V2.Init();

            //await Database.MigrateAsync();
        }
    }

    public static string File { get; protected set; }
    public static bool Initialized { get; protected set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite($"Filename={File}");
    }

    #endregion

    public void Reload()
    {
        Database.CloseConnection();
        Database.OpenConnection();
    }
    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MoviePersonRole> MoviePersonRoles { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<GenreMovie> GenreMovies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("genre");

            entity.HasIndex(e => e.GenreName, "IX_genre_genre_name").IsUnique();

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.GenreName).HasColumnName("genre_name");

        });

        modelBuilder.Entity<GenreMovie>(entity =>
        {
            entity.ToTable("genre_movie");

            entity.HasKey(e => new { e.GenreId, e.MovieId });

            entity.Property(e => e.GenreId).HasColumnName("gener_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");

            entity.HasOne(e => e.Movie)
                  .WithMany(m => m.GenreMovies)
                  .HasForeignKey(e => e.MovieId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Genre)
                  .WithMany(g => g.GenreMovies)
                  .HasForeignKey(e => e.GenreId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("movie");

            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ReleaseYear).HasColumnName("release_year");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.TitleImg).HasColumnName("title_img");
        });

        modelBuilder.Entity<MoviePersonRole>(entity =>
        {
            entity.HasKey(e => new { e.MovieId, e.PersonId, e.RoleId });

            entity.ToTable("movie_person_role");

            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Movie).WithMany(p => p.MoviePersonRoles).HasForeignKey(d => d.MovieId);

            entity.HasOne(d => d.Person).WithMany(p => p.MoviePersonRoles).HasForeignKey(d => d.PersonId);

            entity.HasOne(d => d.Role).WithMany(p => p.MoviePersonRoles).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("person");

            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.PersonName).HasColumnName("person_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("role");

            entity.HasIndex(e => e.RoleName, "IX_role_role_name").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName).HasColumnName("role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
