using PMS_lab1.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PMS_lab1.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}