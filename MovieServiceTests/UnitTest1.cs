using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using MauiFilmLibrary.Models;
using MauiFilmLibrary.Model;

namespace MovieServiceTests
{
    public class MovieServiceTests
    {
        private readonly Mock<MovieDatabaseContext> _mockDbContext;
        private readonly MovieService _movieService;

        public MovieServiceTests()
        {
            // Создаём мок для DbContext.
            _mockDbContext = new Mock<MovieDatabaseContext>(new object[] { "Test.db3" });

            _movieService = new MovieService(_mockDbContext.Object);
        }

        [Fact]
        public async Task SearchMoviesAsync_ShouldReturnMovies_WhenSearchQueryIsMatched()
        {
            var searchQuery = "action";

            // Подготавливаем тестовые данные
            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "Action Movie",
                    Geners = new List<Genre> { new Genre { GenreName = "Action" } },
                    MoviePersonRoles = new List<MoviePersonRole>
                    {
                        new MoviePersonRole { Person = new Person { PersonName = "Actor1" } }
                    }
                },
                new Movie
                {
                    Title = "Drama Movie",
                    Geners = new List<Genre> { new Genre { GenreName = "Drama" } },
                    MoviePersonRoles = new List<MoviePersonRole>
                    {
                        new MoviePersonRole { Person = new Person { PersonName = "Actor2" } }
                    }
                }
            };

            // Создаём фейковый DbSet для Movies
            var moviesDbSetMock = CreateMockDbSet(movies);
            _mockDbContext.Setup(db => db.Movies).Returns(moviesDbSetMock.Object);

            // Act
            var result = await _movieService.SearchMoviesAsync(searchQuery);

            // Assert
            Assert.Single(result);  // Должен вернуть только один фильм, соответствующий запросу
            Assert.Equal("Action Movie", result.First().Title);
        }

        [Fact]
        public async Task SearchMoviesAsync_ShouldReturnEmptyList_WhenNoMoviesMatch()
        {
            // Arrange
            var searchQuery = "Sci-Fi";

            // Тестовые данные без совпадений
            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "Action Movie",
                    Geners = new List<Genre> { new Genre { GenreName = "Action" } },
                    MoviePersonRoles = new List<MoviePersonRole>
                    {
                        new MoviePersonRole { Person = new Person { PersonName = "Actor1" } }
                    }
                },
                new Movie
                {
                    Title = "Drama Movie",
                    Geners = new List<Genre> { new Genre { GenreName = "Drama" } },
                    MoviePersonRoles = new List<MoviePersonRole>
                    {
                        new MoviePersonRole { Person = new Person { PersonName = "Actor2" } }
                    }
                }
            };

            // Создаём фейковый DbSet для Movies
            var moviesDbSetMock = CreateMockDbSet(movies);
            _mockDbContext.Setup(db => db.Movies).Returns(moviesDbSetMock.Object);

            // Act
            var result = await _movieService.SearchMoviesAsync(searchQuery);

            // Assert
            Assert.Empty(result);  // Ожидаем, что список будет пустым
        }

        /// <summary>
        /// Вспомогательный метод для создания мокированного DbSet из списка данных.
        /// </summary>
        private Mock<DbSet<T>> CreateMockDbSet<T>(List<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSetMock;
        }
    }
}
