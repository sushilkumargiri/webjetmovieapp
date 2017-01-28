using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebjetMovieAppBL
{
    public interface IMovie
    {
        List<MovieDetail> GetCheapestMovies(List<MovieDetail> movies,int count);
    }
}
