using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.DataBase
{
    public class AdminDB
    {
        public int Id { get; set; }
        public int AdminCode { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}