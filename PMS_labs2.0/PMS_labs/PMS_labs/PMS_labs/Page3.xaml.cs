using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using PMS_labs.Models;
using PMS_labs.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMS_labs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
        private List<Movie> movies;
        private ObservableCollection<Movie> display;
        //private Movie _movie;
        string API_KEY = "7e9fe69e";
        private const string _searchUrl = "https://www.omdbapi.com/?apikey={0}&s={1}&page=1";
        private const string _detailsUrl = "https://www.omdbapi.com/?apikey={0}&i={1}";

        private readonly HttpClient _client;
        public Page3()
        {
            InitializeComponent();
            _client = new HttpClient();
            movies = new List<Movie>();
            display = new ObservableCollection<Movie>();

            moviesView.ItemsSource = display;

           /* var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Properties.Resources)).Assembly;
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
            }*/
        }

        private void AddMovie(Movie movie)
        {
            display.Add(movie);
        }

        private void DeleteMovie(Movie movie)
        {
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
                var response = await _client.GetAsync(string.Format(_detailsUrl, API_KEY, movie.imdbID));

                response.EnsureSuccessStatusCode();

                using var data = await response.Content.ReadAsStreamAsync();
                using var streamReader = new StreamReader(data);
                using var reader = new JsonTextReader(streamReader);

                var a = await JObject.LoadAsync(reader);
                
                    var Info = new MovieInfo
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
                

                await Navigation.PushAsync(new MovieInfoPage(Info));
                //Shell.Current.GoToAsync("MovieInfoPage");
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Error", $"Details for movie {movie.imdbID} not found", "OK");
            }
        }
        private async void OnTextChanged(object sender, EventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var search = searchBar.Text;
            display.Clear();
            if (search.Length < 3) return;

            var text = search.Replace(' ', '+');

            var response = await _client.GetAsync(string.Format(_searchUrl, API_KEY, text));

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "Error", "Ok");
                return;
            }

            using var data = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(data);
            using var reader = new JsonTextReader(streamReader);

            var a = await JObject.LoadAsync(reader);

            var t = a["Search"].Select(v => new Movie
            {
                Title = (string)v["Title"],
                Year = (string)v["Year"],
                imdbID = (string)v["imdbID"],
                Type = (string)v["Type"],
                Poster = (string)v["Poster"]
            });
            foreach (var b in t)
            {
                AddMovie(b);
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