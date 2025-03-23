using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MauiFilmLibrary.Models;
using MauiFilmLibrary.View;

namespace MauiFilmLibrary.ViewModel
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private readonly int _searchQuantity = 5;
        private readonly Model.MovieService _movieService;
        private string _searchQuery;
        private bool _isMoviePageVisible = false;
        
        private ObservableCollection<Movie> _suggestions = new();
        public ObservableCollection<Movie> Movies { get; set; } = new();
        public ObservableCollection<Movie> Suggestions
        {
            get => _suggestions;
            set { _suggestions = value; OnPropertyChanged(); }
        }
        public ICommand SearchCommand { get; }
        public ICommand CloseMovieCommand { get; }
        public ICommand OpenMovieCommand { get; }
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
        public SearchViewModel(Model.MovieService movieService) //рег комманд
        {
            _movieService = movieService;
            SearchCommand = new Command(async () => await SearchMoviesAsync());
            CloseMovieCommand = new Command(() => IsMoviePageVisible = false);
            OpenMovieCommand = new Command<Movie>(async (movie) => await OpenMovie(movie));
        }

        private async Task SearchMoviesAsync()
        {
            var movies = await _movieService.SearchMoviesAsync(SearchQuery);
            Movies.Clear();
            IsMoviePageVisible = false;

            foreach (var movie in movies)
            {
                Movies.Add(movie);
            }
        }

        private async Task OpenMovie(Movie movie)
        {
            if (movie == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(new MovieView(movie));
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
            foreach (var movie in movies)
            {
                Suggestions.Add(movie);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}