using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebjetMovieAppBL
{
    public class Constants
    {
        public static readonly int CacheMinuteDuration = 30;

        public static readonly string WebjetTokenName = "x-access-token";
        public static readonly string WebjetTokenValue = "sjd1HfkjU83ksdsm3802k";

        public static readonly string WebjetMoviesURL = "http://webjetapitest.azurewebsites.net/api/filmworld/movies";


        public static readonly string WebjetMovieURL = "http://webjetapitest.azurewebsites.net/api/filmworld/movie/{0}";


    }
}
