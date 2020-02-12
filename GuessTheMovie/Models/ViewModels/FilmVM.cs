using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.ViewModels
{
    public class FilmVM
    {
        public string FilmCode { get; set; }
        public string FilmName { get; set; }
        public int FilmYear { get; set; }
        public string FilmGenre { get; set; }
        public string FilmImage { get; set; }
        public bool Correct { get; set; }
    }
}