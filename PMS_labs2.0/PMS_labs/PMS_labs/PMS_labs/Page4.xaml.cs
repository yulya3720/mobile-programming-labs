using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PMS_labs.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Media;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;

namespace PMS_labs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page4 : ContentPage
    {
        private const string _requestUrl = "https://pixabay.com/api/?key={0}&q={1}&image_type=photo&per_page={2}";
        private const string _apiKey = "19193969-87191e5db266905fe8936d565";
        private const string _request = "small+animals";
        private const int _count = 18;

        private HttpClient _client;
        public Page4()
        {
            InitializeComponent();
            _client = new HttpClient();
        }
         protected override async void OnAppearing()
         {
            base.OnAppearing();

            base.OnAppearing();

            var response = await _client.GetAsync(string.Format(_requestUrl, _apiKey, _request, _count));

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "Error", "Ok");
                return;
            }

            using var data = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(data);
            using var reader = new JsonTextReader(streamReader);

            var a = await JObject.LoadAsync(reader);

            foreach (var u in a["hits"].Select(v => v["webformatURL"]))
            {
                galleryLayout.Add(ImageSource.FromUri(new Uri((string)u)));
            }
         }

            private async void OnAddItem(object sender, EventArgs e)
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Error", "Picking a photo is not supported on your device", "OK");
                    return;
                }

                NavigationPage.SetHasNavigationBar(this, false);

                var img = await CrossMedia.Current.PickPhotoAsync();

                NavigationPage.SetHasNavigationBar(this, true);

                if (img is null) return;
                galleryLayout.Add(ImageSource.FromStream(() => img.GetStream()));
            }

 
    }
    
}