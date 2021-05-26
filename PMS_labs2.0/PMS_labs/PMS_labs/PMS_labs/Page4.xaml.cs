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
           
            public Page4()
            {
                InitializeComponent();
            }
             protected override async void OnAppearing()
             {
                base.OnAppearing();

                for(int i = 1; i <= 6; i++)
                {
                    galleryLayout.Add(ImageSource.FromResource($"PMS_labs.Data.Gallery.{i}.PNG", typeof(Page4).GetTypeInfo().Assembly));
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