using GuessTheMovie.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using GuessTheMovie.Models.ViewModels;
using System.Threading.Tasks;
using GuessTheMovie.Models.MovieDataBase;

namespace GuessTheMovie.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Update()
        {
            await AdminFilms.UpdateDatabase();

            FilmInfo.CheckSimilarFilms();

            return RedirectToAction("Index");
        }

        public ActionResult Index(int? page)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Title = "Home Page";

                int pageSize = 20;
                int pageNumber = (page ?? 1);

                var data = AdminFilms.GetFilms();

                return View(data.ToPagedList(pageNumber, pageSize));
            }
            else
                return RedirectToAction("Index", "Admin");
        }

        public ActionResult Edit(int id)
        {
            var data = AdminFilms.GetFilm(id);

            return View(data);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(AdminFilmsVM film)
        {
            var flag = AdminFilms.EditFilm(film);

            if (flag != null)
                return RedirectToAction("Index");
            else
                return View(flag);
        }

        public ActionResult ClearDataBase()
        {
            AdminFilms.ClearDataBase();

            return RedirectToAction("Index");
        }
    }
}
