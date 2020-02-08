using GuessTheMovie.Cookies;
using GuessTheMovie.Models.Admin;
using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuessTheMovie.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminVM admin)
        {
            if (ModelState.IsValid)
            {
                admin = Admin.Login(admin);

                if (admin != null)
                {
                    AdminCookie adminCookie = new AdminCookie();

                    adminCookie.SetAdminCookie(admin);
                }
                else
                    ModelState.AddModelError("", "Логин или пароль неправильный!");
            }

            return View();
        }
    }
}