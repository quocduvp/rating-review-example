using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rating_review_example.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Rating()
        {
            ViewBag.Title = "Rating Review Services";
            return View();
        }

        public ActionResult ReviewComplete()
        {
            ViewBag.Title = "Review Complete";

            return View();
        }
    }
}
