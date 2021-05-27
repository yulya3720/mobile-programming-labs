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

        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var lv = (ListView)sender;
            var movie = (Movie)lv.SelectedItem;

            MovieInfo info = null;
            string id = movie.imdbID;
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
                        Actors = (string)a["Actors"],
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


                await App.Db.InsertDetailsAsync(info);
                //Shell.Current.GoToAsync("MovieInfoPage");
            }
            catch 
            {
                info = await App.Db.GetDetailsByIdAsync(id);
            }
            if (info is null)
            {
                await DisplayAlert("Error", $"Details for movie {movie.imdbID} not found", "OK");
                return;
            }
            await Navigation.PushAsync(new MovieInfoPage(info));
        }
        private async void OnTextChanged(object sender, EventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var search = searchBar.Text;
            display.Clear();
            if (search.Length < 3) return;

            var text = search.Replace(' ', '+');

            //   var response = await _client.GetAsync(string.Format(_searchUrl, API_KEY, text));
            IEnumerable<Movie> movies = new List<Movie>();
            try
            {
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
            }
            catch
            {
                movies = await App.Db.GetMoviesBySearchStringAsync(text);
            }

            if (movies.Count() == 0)
            {
                moviesView.IsVisible = false;
                notFound.IsVisible = true;
            }
            else
            {
                moviesView.IsVisible = true;
                notFound.IsVisible = false;

                foreach (var m in movies)
                {
                    await App.Db.InsertMovieAsync(m);
                    AddMovie(m);
                }
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