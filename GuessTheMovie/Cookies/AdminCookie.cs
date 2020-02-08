using GuessTheMovie.Hashing;
using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Cookies
{
    public class AdminCookie
    {
        readonly string sharedKey = "123";

        readonly string adminSession = "admin_session";

        public void SetAdminCookie(AdminVM admin)
        {
            string adminId = Hash.EncryptStringAES(admin.AdminCode.ToString(), sharedKey);

            HttpCookie cookie = new HttpCookie(adminSession)
            {
                Value = adminId
            };

            // Этот cookie-набор будет оставаться 
            // действительным в течение одного года
            cookie.Expires = DateTime.Now.AddDays(7);

            // Добавить куки в ответ
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}