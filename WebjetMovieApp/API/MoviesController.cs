using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebjetMovieAppBL;
using WebjetMovieAppBL.Caching;

namespace WebjetMovieApp.API
{
    public class MoviesController : ApiController
    {
        // GET api/<controller>
        public async Task<HttpResponseMessage> Get()
        {
            MovieList movieList = new MovieList();
            movieList = GlobalCachingProvider.Instance.GetItem("AllMovieData", remove: false) as MovieList;
            if (movieList != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, movieList);
            }
            else
            {
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add(Constants.WebjetTokenName, Constants.WebjetTokenValue);
                        HttpResponseMessage response = await httpClient.GetAsync(Constants.WebjetMoviesURL);
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        Logger.Write(LogLevel.INFO, "Movies data get success");
                        movieList = JsonConvert.DeserializeObject<MovieList>(result);
                        GlobalCachingProvider.Instance.AddItem("AllMovieData", movieList);
                        return Request.CreateResponse(HttpStatusCode.OK, movieList);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(LogLevel.ERROR, ex.Message);
                    return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Could not get movies!");
                }
            }
        }
        // GET api/<controller>/xyz
        public async Task<HttpResponseMessage> Get(string movieId)
        {
            MovieDetail movie = new MovieDetail();
            movie = GlobalCachingProvider.Instance.GetItem(movieId, remove: false) as MovieDetail;
            if (movie!=null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, movie);
            }
            else
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add(Constants.WebjetTokenName, Constants.WebjetTokenValue);
                    HttpResponseMessage response = await httpClient.GetAsync(string.Format(Constants.WebjetMovieURL,movieId));
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    Logger.Write(LogLevel.INFO, string.Format("Movie data retrieved for ID: {0}", movieId));
                    movie = JsonConvert.DeserializeObject<MovieDetail>(result);
                    GlobalCachingProvider.Instance.AddItem(movieId, movie);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, movie);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(LogLevel.ERROR, ex.Message);
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, string.Format("Could not get movie for ID: {0}", movieId));
            }
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