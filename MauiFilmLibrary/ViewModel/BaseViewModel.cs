using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiFilmLibrary.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public ICommand CloseCommand { get; }
        public ICommand OpenCommand { get; }

        public BaseViewModel() 
        {
            CloseCommand = new Command(async () => await BackCommand());
        }
        private async Task BackCommand() => await Application.Current.MainPage.Navigation.PopAsync();
        private async Task OpenView(ContentPage view) => await Application.Current.MainPage.Navigation.PushAsync(view);

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
