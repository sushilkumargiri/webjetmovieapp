using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebjetMovieAppBL
{
    public class MovieBL : IMovie
    {
        public List<MovieDetail> GetCheapestMovies(List<MovieDetail> movies,int count)
        {
            return movies.OrderBy(m => Convert.ToDecimal(m.Price)).Take(count).ToList();
        }
    }
}
