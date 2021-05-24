using PMS_labs.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMS_labs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMoviePage : ContentPage
    {
        private readonly Action<Movie> method;
        public AddMoviePage(Action<Movie> invokable)
        {
            method = invokable;
            InitializeComponent();
        }
        private async void OnSubmit(object sender, EventArgs e)
        {
            var title = titleEntry.Text;
            var year = yearEntry.Text;
            var imdbid = imdbidEntry.Text;
            var type = typeEntry.Text;
            if (!(imdbid[0] == 't') || !(imdbid[1] == 't') || imdbid.Length != 9)
            {
                await DisplayAlert("Error", $"Invalid : {imdbid}", "OK");
                return;
            }

            if (!decimal.TryParse(yearEntry.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out var price))
            {
                await DisplayAlert("Error", $"Invalid year: {yearEntry.Text}", "OK");
                return;
            }

            var movie = new Movie
            {
                Poster = "",
                imdbID = imdbid,
                Year = year,
                Type = type,
                Title = title
            };

          //  Console.WriteLine(movie);

            method(movie);
            await Navigation.PopAsync();
        }
    }
    
}