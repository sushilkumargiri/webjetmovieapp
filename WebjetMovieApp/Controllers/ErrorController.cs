using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebjetMovieApp.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController() { }
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error(string action, string message)
        {
            ViewBag.action = action;
            ViewBag.message = action;
            return View();
        }
    }
}