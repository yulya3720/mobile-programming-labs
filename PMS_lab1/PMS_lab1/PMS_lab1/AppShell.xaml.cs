using PMS_lab1.ViewModels;
using PMS_lab1.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PMS_lab1
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
