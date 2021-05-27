using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PMS_labs.Models;

namespace PMS_labs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieInfoPage : ContentPage
    {
        private MovieInfo info;
        public MovieInfoPage(MovieInfo info)
        {
            this.info = info;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            movieTitle.Text = info.Title;
            movieDirector.Text = $"Director: {info.Director}";
            movieYear.Text = info.Year.ToString();
            movieGenre.Text = info.Genre;
            movieReleased.Text = $"Released: {info.Released}";
            movieRuntime.Text = info.Runtime;
            movieWriter.Text = $"Writer: {info.Writer}";
            movieActors.Text = "Actors: " + string.Join(", ", info.Actors);
            movieLanguage.Text = info.Language;
            movieCountry.Text = info.Country;
            movieAwards.Text = $"Awards: {info.Awards}";
            movieProduction.Text = $"Production: {info.Production}";
            movieType.Text = info.Type;
            movieRated.Text = info.Rated;
            movieimdbRating.Text = info.imdbRating;
            movieimdbVotes.Text = info.imdbVotes;
            movieimdbID.Text = info.imdbID;
            moviePlot.Text = info.Plot;
            moviePoster.Source = new UriImageSource
            {
                CachingEnabled = true,
                Uri = new Uri(info.Poster)
            };
        }
    }
}