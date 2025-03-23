using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MauiFilmLibrary.Models;

namespace MauiFilmLibrary.ViewModel
{
    public class MovieViewModel
    {
        public Movie Movie { get; }
        public ICommand BackCommand { get; }

        public MovieViewModel(Movie movie)
        {
            Movie = movie;
            BackCommand = new Command(async () => await CloseMovie());
        }
        private async Task CloseMovie()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}