using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebjetMovieAppBL
{
    public class APICredentials: IAPICredentials
    {
        public Dictionary<string, string> GetCredentials()
        {
            return new Dictionary<string, string>
        {
            { "x-access-token", "sjd1HfkjU83ksdsm3802k" }
        };
        }
        public string GetMovieListApiURL()
        {
            return "http://webjetapitest.azurewebsites.net/api/filmworld/movies";
        }
        public string GetMovieDetailApiURL(string movieId)
        {
            return "http://webjetapitest.azurewebsites.net/api/filmworld/movie/" + movieId;
        }

    }
}
