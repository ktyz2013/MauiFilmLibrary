using MauiFilmLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiFilmLibrary.Model
{
    public class MovieService
    {
        private readonly MovieDatabaseContext _dbContext;

        public MovieService(MovieDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MovieDto?> GetMovieFromIdAsync(int movieId)
        {
            var movieData = await _dbContext.Movies
                .AsNoTracking()
                .Where(m => m.MovieId == movieId)
                .Select(m => new
                {
                    m.MovieId,
                    m.Title,
                    m.Description,
                    m.ReleaseYear,
                    m.TitleImg,
                    Genres = m.GenreMovies
                        .Select(gm => gm.Genre.GenreName)
                        .ToList(),
                    Persons = m.MoviePersonRoles
                        .Select(mp => new
                        {
                            mp.Person.PersonName,
                            mp.Role.RoleName
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (movieData == null)
                return null;

            return new MovieDto
            {
                MovieId = movieData.MovieId,
                Title = movieData.Title,
                Description = movieData.Description,
                ReleaseYear = movieData.ReleaseYear,
                TitleImg = movieData.TitleImg,
                GenresDto = movieData.Genres
                    .Select(gName => new GenerDto { GenreName = gName })
                    .ToList(),
                PersonDto = movieData.Persons
                    .Select(p => new PersonDto
                    {
                        PersonName = p.PersonName,
                        PersonRole = p.RoleName
                    })
                    .ToList()
            };
        }


        public async Task<List<MovieDto>> SearchMoviesAsync(string searchQuery, int limit = int.MaxValue)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
                return new List<MovieDto>();

            var searchTerms = searchQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var moviesData = await _dbContext.Movies
                .AsNoTracking()
                .Include(m => m.GenreMovies)
                    .ThenInclude(gm => gm.Genre)
                .Include(m => m.MoviePersonRoles)
                    .ThenInclude(mpr => mpr.Person)
                .Include(m => m.MoviePersonRoles)
                    .ThenInclude(mpr => mpr.Role)
                .Where(m => searchTerms.Any(term =>
                    EF.Functions.Like(m.Title, "%" + term + "%") ||
                    m.MoviePersonRoles.Any(mpr => EF.Functions.Like(mpr.Person.PersonName, "%" + term + "%")) ||
                    m.GenreMovies.Any(gm => EF.Functions.Like(gm.Genre.GenreName, "%" + term + "%"))
                ))
                .Take(limit)
                .Select(m => new
                {
                    m.MovieId,
                    m.Title,
                    m.TitleImg,
                    m.ReleaseYear,
                    Genres = m.GenreMovies
                        .Select(gm => gm.Genre.GenreName)
                        .ToList(),
                    Persons = m.MoviePersonRoles
                        .Select(p => new
                        {
                            p.Person.PersonName,
                            RoleName = p.Role.RoleName
                        })
                        .ToList()
                })
                .ToListAsync();

            if (moviesData == null)
                return null;

            return moviesData.Select(m => new MovieDto
            {
                MovieId = m.MovieId,
                Title = m.Title,
                TitleImg = m.TitleImg,
                ReleaseYear = m.ReleaseYear,
                GenresDto = m.Genres.Select(gName => new GenerDto { GenreName = gName }).ToList(),
                PersonDto = m.Persons.Select(p => new PersonDto
                {
                    PersonName = p.PersonName,
                    PersonRole = p.RoleName
                }).ToList()
            }).ToList();
        }
    }
}
