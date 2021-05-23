using System;
using System.Collections.Generic;
using System.Text;

namespace PMS_labs.Models
{
    class Movie
    {
        private string _poster;

        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get => _poster; set => _poster = value == "" ? null : value; }

    }
}
