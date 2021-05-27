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
        public static AppDataStore Db { get; private set; }
        public App()
        {
            InitializeComponent();

           // DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            Db = await AppDataStore.Create();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
