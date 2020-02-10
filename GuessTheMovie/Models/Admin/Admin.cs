using GuessTheMovie.Models.DataBase;
using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.Admin
{
    public class Admin
    {
        public static AdminVM Login(AdminVM admin)
        {
            AdminVM user = new AdminVM();

            using (DataBaseContext db = new DataBaseContext())
            {
                user = db.AdminDB
                    .Select(
                    x => new AdminVM
                    {
                        AdminCode = x.AdminCode,
                        Login = x.Login,
                        Password = x.Password,
                    }).FirstOrDefault();
            }

            if (user.Login != admin.Login && user.Password != admin.Password)
                user = null;
            else
                user.RememberMe = admin.RememberMe;
            return user;
        }
    }
}