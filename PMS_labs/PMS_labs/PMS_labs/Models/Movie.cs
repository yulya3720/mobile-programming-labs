using System;
using System.Collections.Generic;
using System.Text;

namespace PMS_labs.Models
{
    class Movie
    {
        private string _image;

        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Image { get => _image; set => _image = value == "" ? null : value; }

    }
}
