using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebjetMovieAppBL
{
    public interface IAPICredentials
    {
        Dictionary<string,string> GetCredentials();
        string GetMovieListApiURL();
        string GetMovieDetailApiURL(string movieId);
    }
}
