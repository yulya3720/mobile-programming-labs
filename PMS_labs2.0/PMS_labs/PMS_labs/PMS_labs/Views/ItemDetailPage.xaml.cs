using PMS_labs.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PMS_labs.Views
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