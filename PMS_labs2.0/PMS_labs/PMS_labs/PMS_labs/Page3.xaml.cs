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


namespace PMS_labs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
        private List<Movie> movies;
        public Page3()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Properties.Resources)).Assembly;

            using (var stream = assembly.GetManifestResourceStream("PMS_labs.Properties.MoviesList.json"))
            using (var reader = new StreamReader(stream))
            {
                var str = reader.ReadToEnd();
                JObject a = JObject.Parse(str);
                movies = a["Search"].Select(v => new Movie { Title = (string)v["Title"], Year = (string)v["Year"], imdbID = (string)v["imdbID"], Type = (string)v["Type"], Poster = (string)v["Poster"] }).ToList();
            }

            moviesView.ItemsSource = movies;
        }
    }
}