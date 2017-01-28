using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebjetMovieAppBL;
using WebjetMovieAppBL.Caching;

namespace WebjetMovieApp.API
{
    public class CheapestMovieController : ApiController
    {

        private readonly IMovie _movieBL;
        public CheapestMovieController() { }
        public CheapestMovieController(IMovie movie)
        {
            _movieBL = movie;
        }
        // GET api/<controller>
        public async Task<HttpResponseMessage> Get()
        {
            string url= HttpContext.Current.Request.Url.AbsoluteUri.Replace("/api/cheapestmovie","");
            MovieList movieList = await GetMovies(url);

            List<MovieDetail> movieDetailList = new List<MovieDetail>();
            foreach (Movie movie in movieList.Movies)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("api/movies/?movieId=" + movie.ID).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var movieDetail = JsonConvert.DeserializeObject<MovieDetail>(result);
                        movieDetailList.Add(movieDetail);
                    }
                }
            }
            List<MovieDetail> Movies = _movieBL.GetCheapestMovies(movieDetailList,1);
            return Request.CreateResponse(HttpStatusCode.OK, Movies);
        }

        private async Task<MovieList> GetMovies(string url)
        {
            var movieList = new MovieList();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("api/movies").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    movieList = JsonConvert.DeserializeObject<MovieList>(result);
                }
            }
            return movieList;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}