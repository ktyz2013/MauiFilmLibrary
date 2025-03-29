using MauiFilmLibrary.ViewModel;

namespace MauiFilmLibrary.View
{
    public partial class MovieView : ContentPage
    {
        public MovieView(MovieViewModel movieViewModel)
        {
            InitializeComponent();
            BindingContext = movieViewModel;
        }
    }
}
