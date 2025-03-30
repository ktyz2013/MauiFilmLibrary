using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiFilmLibrary.Model;
using MauiFilmLibrary.View;

namespace MauiFilmLibrary.ViewModel
{
    public class MovieViewModel: BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private Model.MovieService? _movieService;

        public int Id { get; }
        public string Title { get; }
        public string? Description { get; }
        public string ReleaseYear { get; }
        public ImageSource? TitleImg { get; }
        public string Genres { get; }
        public string Actors { get; }

        public ICommand OpenMovieCommand { get; }

        public MovieViewModel(MovieDto dto, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Id = dto.MovieId;
            Title = dto.Title ?? "Неизвестное название";
            Description = dto.Description ?? "Нет описания";
            ReleaseYear = dto.ReleaseYear?.ToString() ?? "Неизвестная дата";
            TitleImg = dto.TitleImg != null
                ? ImageSource.FromStream(() => new MemoryStream(dto.TitleImg))
                : ImageSource.FromFile("not_found.png");
            Genres = dto.GenresDto.Any() ? string.Join(", ", dto.GenresDto.Select(g => g.GenreName)) : "Неизвестный жанр";
            Actors = dto.PersonDto.Any() ? string.Join(", ", dto.PersonDto.Select(p => p.PersonName)) : "Неизвестные актёры";

            OpenMovieCommand = new Command<int>(async (movieId) => await OpenMovie(movieId));
        }
        private async Task OpenMovie(int movieId)
        {
            if (movieId == 0) return;

            _movieService ??= _serviceProvider.GetRequiredService<Model.MovieService>();

            var movieDto = await _movieService.GetMovieFromIdAsync(movieId);
            if (movieDto == null) return;

            var movieViewModel = new MovieViewModel(movieDto, _serviceProvider);
            await Application.Current.MainPage.Navigation.PushAsync(new MovieView(movieViewModel));
        }
    }

}