using System;
using System.Collections.Generic;
using System.Text;
using SQLitePCL;
using SQLite;

namespace PMS_labs.Models
{
    public class Movie 
    {

        [PrimaryKey]
        public string imdbID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        /* public string Poster { get => _poster; set => _poster = value == "" ? null : value; }
         public bool Equals(Movie other)
         {
             return Title == other.Title && Year == other.Year && imdbID == other.imdbID && Type == other.Type;
         }

         public override bool Equals(object obj)
         {
             return obj is Movie m && Equals(m);
         }

         public static bool operator ==(Movie lhs, Movie rhs)
         {
             return lhs is Movie m1 && rhs is Movie m2 && m1.Equals(m2);
         }

         public static bool operator !=(Movie lhs, Movie rhs)
         {
             return lhs is Movie m1 && rhs is Movie m2 && !m1.Equals(m2);
         }

         public override int GetHashCode()
         {
             return HashCode.Combine(Title, Year, imdbID, Type);
         }

         public override string ToString()
         {
             return $"Movie {{Title = {Title},  Year = {Year}, imdbID = {imdbID}, Type = {Type}}}";
         }*/
    }
}
