using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebjetMovieAppBL
{
    public class Movie
    {
        public string Poster { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Year { get; set; }
        public string ID { get; set; }
    }
    public class MovieList
    {
        public List<Movie> Movies { get; set; }
    }
}
