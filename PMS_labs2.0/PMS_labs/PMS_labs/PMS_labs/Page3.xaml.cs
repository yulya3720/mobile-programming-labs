using PMS_labs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Linq;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Collections.ObjectModel;

namespace PMS_labs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
        private List<Movie> movies;
        private ObservableCollection<Movie> display;
        private Movie _movie;
        public Page3()
        {
            InitializeComponent();
            movies = new List<Movie>();
            display = new ObservableCollection<Movie>();

            moviesView.ItemsSource = display;

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Properties.Resources)).Assembly;
            var c = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            c.NumberFormat.CurrencySymbol = "$";
            CultureInfo.CurrentCulture = c;

            using (var stream = assembly.GetManifestResourceStream("PMS_labs.Properties.MoviesList.json"))
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                var a = JObject.Load(reader);
                a["Search"].Select(v => new Movie
                {
                    Title = (string)v["Title"],
                    Year = (string)v["Year"],
                    imdbID = (string)v["imdbID"],
                    Type = (string)v["Type"],
                    Poster = (string)v["Poster"]
                }).OrderBy(x => x.Title).ToList().ForEach(AddMovie);
            }
        }

        private void AddMovie(Movie movie)
        {
            _movie = movie;
            movies.Add(movie);
            display.Add(movie);

        }

        private void DeleteMovie(Movie movie)
        {
            movies.Remove(movie);
            display.Remove(movie);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
/*
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Properties.Resources)).Assembly;

            using (var stream = assembly.GetManifestResourceStream("PMS_labs.Properties.MoviesList.json"))
            using (var reader = new StreamReader(stream))
            {
                var str = reader.ReadToEnd();
                JObject a = JObject.Parse(str);
                movies = a["Search"].Select(v => new Movie { Title = (string)v["Title"], Year = (string)v["Year"], imdbID = (string)v["imdbID"], Type = (string)v["Type"], Poster = (string)v["Poster"] }).ToList();
            }

            moviesView.ItemsSource = movies;*/
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var lv = (ListView)sender;
            var movie = (Movie)lv.SelectedItem;

            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Properties.Resources)).Assembly;

                MovieInfo Info;

                using (var stream = assembly.GetManifestResourceStream($"PMS_labs.Data.{movie.imdbID}.json"))
                using (var streamReader = new StreamReader(stream))
                using (var reader = new JsonTextReader(streamReader))
                {
                    var a = await JObject.LoadAsync(reader);
                    Info = new MovieInfo
                    {
                        Actors = ((string)a["Actors"]).Split(',').Select(x => x.Trim()).ToArray(),
                        Rated = (string)a["Rated"],
                        Poster = (string)a["Poster"],
                        Released = (string)a["Released"],                        
                        Runtime = (string)a["Runtime"],
                        Genre = (string)a["Genre"],
                        Director = (string)a["Director"],
                        Writer = (string)a["Writer"],
                        Plot = (string)a["Plot"],
                        Language = (string)a["Language"],
                        Country = (string)a["Country"],
                        Awards = (string)a["Awards"],
                        imdbRating = (string)a["imdbRating"],
                        imdbVotes = (string)a["imdbVotes"],
                        imdbID = (string)a["imdbID"],
                        Title = (string)a["Title"],
                        Type = (string)a["Type"],
                        Production = (string)a["Production"],
                        Year = int.Parse((string)a["Year"])
                    };
                }

                await Navigation.PushAsync(new MovieInfoPage(Info));
                //Shell.Current.GoToAsync("MovieInfoPage");
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Error", $"Details for movie {movie.imdbID} not found", "OK");
            }
        }
        private void OnTextChanged(object sender, EventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var search = searchBar.Text;

            var t = movies.Where(b => b.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            if (t.Any())
            {
                display.Clear();
                t.ToList().ForEach(i => display.Add(i));
                movieEmpty.IsVisible = false;
                moviesView.IsVisible = true;
            }
            else
            {
                moviesView.IsVisible = false;
                movieEmpty.IsVisible = true;
            }
        }
        private void OnDelete(object sender, EventArgs e)
        {
            var movie = (Movie)((MenuItem)sender).CommandParameter;
            DeleteMovie(movie);
        }
        private async void OnAddItem(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMoviePage(AddMovie));
        }
    }
}