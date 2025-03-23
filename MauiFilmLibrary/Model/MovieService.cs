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

        public async Task<List<Movie>> SearchMoviesAsync(string searchQuery, int limit = 10)
        {
            var searchTerms = searchQuery.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return await _dbContext.Movies
                .Include(m => m.Geners)
                .Include(m => m.MoviePersonRoles)
                    .ThenInclude(mpr => mpr.Person)
                .Where(m => searchTerms.All(term =>
                    m.Title.ToLower().Contains(term) ||
                    m.MoviePersonRoles.Any(mpr => mpr.Person.PersonName.ToLower().Contains(term)) ||
                    m.Geners.Any(g => g.GenreName.ToLower().Contains(term))))
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<Movie>> SearchMoviesAsync(string searchQuery)
        {
            var searchTerms = searchQuery.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return await _dbContext.Movies
                .Include(m => m.Geners)
                .Include(m => m.MoviePersonRoles)
                    .ThenInclude(mpr => mpr.Person)
                .Where(m => searchTerms.All(term =>
                    m.Title.ToLower().Contains(term) ||
                    m.MoviePersonRoles.Any(mpr => mpr.Person.PersonName.ToLower().Contains(term)) ||
                    m.Geners.Any(g => g.GenreName.ToLower().Contains(term))))
                .ToListAsync();
        }
    }

}