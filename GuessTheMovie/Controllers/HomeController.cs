using GuessTheMovie.Models.Films;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuessTheMovie.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Title = "Home Page";

                var data = Films.GetPool();

                return View(data);
            }
            else
                return RedirectToAction("Index", "Admin");
        }
    }
}
