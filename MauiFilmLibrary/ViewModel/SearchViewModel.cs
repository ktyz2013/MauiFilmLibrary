using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MauiFilmLibrary.Model;
using MauiFilmLibrary.View;
using Microsoft.Extensions.DependencyInjection;

namespace MauiFilmLibrary.ViewModel
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly int _searchQuantity = 5;
        private readonly Model.MovieService _movieService;
        private readonly IServiceProvider _serviceProvider;

        private string _searchQuery;
        private bool _isMoviePageVisible = false;
        private ObservableCollection<MovieViewModel> _suggestions = new();

        public ObservableCollection<MovieViewModel> Movies { get; private set; } = new();
        public ObservableCollection<MovieViewModel> Suggestions
        {
            get => _suggestions;
            private set { _suggestions = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }
        public ICommand CloseMovieCommand { get; }
        public bool IsMoviePageVisible
        {
            get => _isMoviePageVisible;
            set { _isMoviePageVisible = value; OnPropertyChanged(); }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                UpdateSuggestions();
            }
        }

        public SearchViewModel(Model.MovieService movieService, IServiceProvider serviceProvider)
        {
            _movieService = movieService;
            _serviceProvider = serviceProvider;

            SearchCommand = new Command(async () => await SearchMoviesAsync());
            CloseMovieCommand = new Command(() => IsMoviePageVisible = false);
        }

        private async Task SearchMoviesAsync()
        {
            if(SearchQuery == null)
                return;
            var movies = await _movieService.SearchMoviesAsync(SearchQuery);
            Movies.Clear();
            IsMoviePageVisible = false;

            var movieViewModelFactory = _serviceProvider.GetRequiredService<Func<MovieDto, MovieViewModel>>();

            foreach (var movieDto in movies)
            {
                Movies.Add(movieViewModelFactory(movieDto));
            }
        }

        public async Task UpdateSuggestions()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Suggestions.Clear();
                return;
            }

            var movies = await _movieService.SearchMoviesAsync(SearchQuery, _searchQuantity);

            Suggestions.Clear();

            var movieViewModelFactory = _serviceProvider.GetRequiredService<Func<MovieDto, MovieViewModel>>();

            foreach (var movieDto in movies)
            {
                Suggestions.Add(movieViewModelFactory(movieDto));
            }   
        }
    }

}