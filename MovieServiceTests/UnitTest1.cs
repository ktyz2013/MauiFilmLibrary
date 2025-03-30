using Microsoft.EntityFrameworkCore;
using Xunit;
using MauiFilmLibrary.Models;
using MauiFilmLibrary.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieServiceTests
{
    public class MovieServiceTests
    {
        private readonly MovieDatabaseContext _dbContext;
        private readonly MovieService _movieService;

        public MovieServiceTests()
        {
            var options = new DbContextOptionsBuilder<MovieDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestMovieDb")
                .Options;

            _dbContext = new MovieDatabaseContext(options);
            _dbContext.Database.EnsureCreated();

            _movieService = new MovieService(_dbContext);
        }

        [Fact]
        public async Task SearchMoviesAsync_ShouldReturnMovies_WhenSearchQueryIsMatched()
        {
            // Arrange
            var searchQuery = "action";

            var actionGenre = new Genre { GenreName = "Action" };
            var dramaGenre = new Genre { GenreName = "Drama" };

            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "Action Movie",
                    GenreMovies = new List<GenreMovie> { new GenreMovie { Genre = actionGenre } },
                    MoviePersonRoles = new List<MoviePersonRole>
                    {
                        new MoviePersonRole { Person = new Person { PersonName = "Actor1" } }
                    }
                },
                new Movie
                {
                    Title = "Drama Movie",
                    GenreMovies = new List<GenreMovie> { new GenreMovie { Genre = dramaGenre } },
                    MoviePersonRoles = new List<MoviePersonRole>
                    {
                        new MoviePersonRole { Person = new Person { PersonName = "Actor2" } }
                    }
                }
            };

            _dbContext.Genres.AddRange(actionGenre, dramaGenre);
            _dbContext.Movies.AddRange(movies);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _movieService.SearchMoviesAsync(searchQuery);

            // Assert
            Assert.Single(result);
            Assert.Equal("Action Movie", result.First().Title);
        }

        [Fact]
        public async Task SearchMoviesAsync_ShouldReturnEmptyList_WhenNoMoviesMatch()
        {
            // Arrange
            var searchQuery = "Sci-Fi";

            var actionGenre = new Genre { GenreName = "Action" };
            var dramaGenre = new Genre { GenreName = "Drama" };

            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "Action Movie",
                    GenreMovies = new List<GenreMovie> { new GenreMovie { Genre = actionGenre } },
                    MoviePersonRoles = new List<MoviePersonRole>
                    {
                        new MoviePersonRole { Person = new Person { PersonName = "Actor1" } }
                    }
                },
                new Movie
                {
                    Title = "Drama Movie",
                    GenreMovies = new List<GenreMovie> { new GenreMovie { Genre = dramaGenre } },
                    MoviePersonRoles = new List<MoviePersonRole>
                    {
                        new MoviePersonRole { Person = new Person { PersonName = "Actor2" } }
                    }
                }
            };

            _dbContext.Genres.AddRange(actionGenre, dramaGenre);
            _dbContext.Movies.AddRange(movies);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _movieService.SearchMoviesAsync(searchQuery);

            // Assert
            Assert.Empty(result);
        }
    }
}
