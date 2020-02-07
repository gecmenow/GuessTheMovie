using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.ViewModels
{
    public class FilmsVM
    {
        public string FilmCode { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Image { get; set; }
    }
}