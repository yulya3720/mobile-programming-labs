using PMS_lab1.Services;
using PMS_lab1.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMS_lab1
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new TabbedPage1();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
