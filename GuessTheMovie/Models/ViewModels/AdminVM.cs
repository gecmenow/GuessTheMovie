using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.ViewModels
{
    public class AdminVM
    {
        public int AdminCode { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}