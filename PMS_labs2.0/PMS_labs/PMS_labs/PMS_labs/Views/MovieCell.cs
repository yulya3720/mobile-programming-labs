using PMS_labs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;


namespace PMS_labs.Views
{
    public class MovieCell : ViewCell
    {
        private Label titleLabel;
        private Label yearLabel;
        private Label imdbidLabel;
        private Label typeLabel;
        private Image poster;

        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(MovieCell), "Title");
        public static readonly BindableProperty YearProperty = BindableProperty.Create("Subtitle", typeof(string), typeof(MovieCell), "Subtitle");
        public static readonly BindableProperty imdbIDProperty = BindableProperty.Create("Price", typeof(string), typeof(MovieCell), "Price");
        public static readonly BindableProperty TypeProperty = BindableProperty.Create("Isbn", typeof(string), typeof(MovieCell), "Isbn");
        public static readonly BindableProperty PosterProperty = BindableProperty.Create("Poster", typeof(string), typeof(MovieCell), "Poster");

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Year
        {
            get => (string)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }

        public string imdbID
        {
            get => (string)GetValue(imdbIDProperty);
            set => SetValue(imdbIDProperty, value);
        }

        public string Type
        {
            get => (string)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public string Poster
        {
            get => (string)GetValue(PosterProperty);
            set => SetValue(PosterProperty, value == "" ? null : value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                titleLabel.Text = Title;
                yearLabel.Text = Year;
                imdbidLabel.Text = imdbID;
                typeLabel.Text = Type;
                poster.Source = ImageSource.FromUri(new Uri(Poster));
            }
        }

        public MovieCell()
        {
            titleLabel = new Label() { HorizontalTextAlignment = TextAlignment.Start };
            yearLabel = new Label() { HorizontalTextAlignment = TextAlignment.Start, FontSize = 12, TextColor = Color.Gray };
            imdbidLabel = new Label() { HorizontalTextAlignment = TextAlignment.End, LineBreakMode = LineBreakMode.NoWrap };
            typeLabel = new Label() { HorizontalTextAlignment = TextAlignment.End, LineBreakMode = LineBreakMode.NoWrap, TextColor = Color.Gray };
            poster = new Image() { HorizontalOptions = LayoutOptions.Start };

            titleLabel.SetBinding(Label.TextProperty, "title");
            yearLabel.SetBinding(Label.TextProperty, "subtitle");
            imdbidLabel.SetBinding(Label.TextProperty, "price");
            typeLabel.SetBinding(Label.TextProperty, "isbn");
            poster.SetBinding(Xamarin.Forms.Image.SourceProperty, "poster");

            var dataLayout = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(5, 1) };
            var metadataLayout = new StackLayout() { HorizontalOptions = LayoutOptions.EndAndExpand, Margin = new Thickness(5, 1) };
            var horizontal = new StackLayout() { Orientation = StackOrientation.Horizontal };
            var wrapper = new StackLayout() { Margin = new Thickness(5, 2) };

            dataLayout.Children.Add(titleLabel);
            dataLayout.Children.Add(yearLabel);

            metadataLayout.Children.Add(imdbidLabel);
            metadataLayout.Children.Add(typeLabel);

            horizontal.Children.Add(poster);
            horizontal.Children.Add(dataLayout);
            horizontal.Children.Add(metadataLayout);

            metadataLayout.MinimumWidthRequest = 100;

            wrapper.Children.Add(horizontal);

            View = wrapper;
        }
    }
}