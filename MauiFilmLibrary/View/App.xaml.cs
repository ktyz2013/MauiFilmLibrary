namespace MauiFilmLibrary.View
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;
            MainPage = new NavigationPage(mainPage);
        }
    }
}
