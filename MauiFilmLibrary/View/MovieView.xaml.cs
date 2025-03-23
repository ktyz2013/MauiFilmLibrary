using MauiFilmLibrary.Model;
using MauiFilmLibrary.Models;
using MauiFilmLibrary.ViewModel;

namespace MauiFilmLibrary.View
{
    public partial class MovieView : ContentPage
    {
        public MovieView(Movie movie)
        {
            InitializeComponent();
            BindingContext = new MovieViewModel(movie);
        }
    }
}
