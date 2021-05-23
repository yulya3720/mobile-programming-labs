using Microcharts;
using PMS_labs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMS_labs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {

        private static readonly PieChart pieChart = new PieChart { Entries = ChartData.PieEntries, LabelMode = LabelMode.None, };
        private static readonly LineChart lineChart = new LineChart { Entries = ChartData.LineEntries, LineSize = 3, LineMode = LineMode.Straight, PointMode = PointMode.None };

        bool switched = false;
        public Page2()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            chartView.Chart = pieChart;
        }

        void OnClick(object sender, EventArgs e)
        {
            if (switched)
            {
                chartView.Chart = pieChart;
                chartSwitch.Text = "Замінити на графік";
                chartPage.Title = "Діаграма";
            }
            else
            {
                chartView.Chart = lineChart;
                chartSwitch.Text = "Замінити на діаграму";
                chartPage.Title = "Графік";
            }

            switched = !switched;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            chartView.HeightRequest = Math.Min(height - 50, width);
        }
    }
}