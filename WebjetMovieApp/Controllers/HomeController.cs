using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using WebjetMovieAppBL;

namespace WebjetMovieApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAPICredentials _iCredentials;
        public HomeController() { }
        public HomeController(IAPICredentials iCredentials)
        {
            _iCredentials = iCredentials;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public async Task<HttpResponseMessage> Movies()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add(_iCredentials.GetCredentials().First().Key, _iCredentials.GetCredentials().First().Value);
                    HttpResponseMessage response = await httpClient.GetAsync(_iCredentials.GetMovieListApiURL());
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    Logger.Write(LogLevel.INFO, "Movies data get success");
                    return request.CreateResponse(System.Net.HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(LogLevel.ERROR, ex.Message);
                return request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Could not get movies!");
            }
        }
        public async Task<HttpResponseMessage> movie(string movieId)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add(_iCredentials.GetCredentials().First().Key, _iCredentials.GetCredentials().First().Value);
                    HttpResponseMessage response = await httpClient.GetAsync(_iCredentials.GetMovieDetailApiURL(movieId));
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    Logger.Write(LogLevel.INFO, string.Format("Movie data retrieved for ID: {0}", movieId));
                    return request.CreateResponse(System.Net.HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(LogLevel.ERROR, ex.Message);
                return request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, string.Format("Could not get movie for ID: {0}", movieId));
            }
        }
    }
}
