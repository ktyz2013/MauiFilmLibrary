using MauiFilmLibrary.ViewModel;

namespace MauiFilmLibrary.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage(SearchViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        private void SearchEntry_Focused(object sender, FocusEventArgs e)
        {
            // Когда поле ввода получает фокус, показываем список
            var viewModel = BindingContext as SearchViewModel;
            if (viewModel != null)
            {
                viewModel.UpdateSuggestions();
            }
        }

        // Обработчик для события Unfocused
        private void SearchEntry_Unfocused(object sender, FocusEventArgs e)
        {
            // Когда поле ввода теряет фокус, скрываем список
            var viewModel = BindingContext as SearchViewModel;
            if (viewModel != null)
            {
                viewModel.Suggestions.Clear();
            }
        }
    }
}
