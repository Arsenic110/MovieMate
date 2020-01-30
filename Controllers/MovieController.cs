using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieMate.Models;

namespace MovieMate.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }
        //Movie/Movies
        public ActionResult Movies(string search)
        {
            ViewBag.search = search;
            return View();
        }
        //Movie/Watch/mvid
        public ActionResult Watch(string id)
        {
            ViewBag.mvid = FileSystem.GetMovieSrc(id);
            return View();
        }
    }
}