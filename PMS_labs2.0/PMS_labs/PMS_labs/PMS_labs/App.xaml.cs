using PMS_labs.Services;
using PMS_labs.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Threading.Tasks;

namespace PMS_labs
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new TabbedPage1();
            MainPage = new AppShell();
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
