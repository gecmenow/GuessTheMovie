using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.ViewModels
{
    public class AdminFilmsVM
    {
        public int FilmId { get; set; }
        public string FilmCode { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Image { get; set; }
        public int ImageCount { get; set; }
        public List<string> FilmImages { get; set; }
        public string SimilarFilmsCode { get; set; }
    }
}