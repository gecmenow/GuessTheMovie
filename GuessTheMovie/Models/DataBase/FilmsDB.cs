using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.DataBase
{
    public class FilmsDB
    {
        public int Id { get; set; }
        public string FilmCode { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Image { get; set; }

        public FilmsDB()
        { }

        public FilmsDB(string filmCode, string genre, string image, string name, int year)
        {
            FilmCode = filmCode;
            Genre = genre;
            Image = image;
            Name = name;
            Year = year;
        }
    }
}