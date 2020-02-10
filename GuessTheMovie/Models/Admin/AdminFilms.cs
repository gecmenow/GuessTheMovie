using GuessTheMovie.Models.DataBase;
using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.Admin
{
    public class AdminFilms
    {
        public static FilmsVM GetFilm(int id)
        {
            FilmsVM data = new FilmsVM();

            using (DataBaseContext db = new DataBaseContext())
            {
                data = db.FilmsDB.Where(f => f.Id == id).Select(f =>
                  new FilmsVM
                  {
                      FilmCode = f.FilmCode,
                      Genre = f.Genre,
                      Image = f.Image,
                      Name = f.Name,
                      Year = f.Year,
                  }).FirstOrDefault();
            }

            return data;
        }
        public static List<FilmsVM> GetFilms()
        {
            List<FilmsVM> data = new List<FilmsVM>();

            using(DataBaseContext db = new DataBaseContext())
            {
                data = db.FilmsDB.Select(f =>
                new FilmsVM
                {
                    FilmId = f.Id,
                    FilmCode = f.FilmCode,
                    Genre = f.Genre,
                    Image = f.Image,
                    Name = f.Name,
                    Year = f.Year,
                }).ToList();
            }

            return data;
        }

        public static FilmsVM EditFilm(FilmsVM film)
        {
            FilmsVM flag = new FilmsVM();

            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var data = db.FilmsDB.Where(f => f.Id == film.FilmId).FirstOrDefault();

                    data.FilmCode = film.FilmCode;
                    data.Genre = film.Genre;
                    data.Image = film.Image;
                    data.Name = film.Name;
                    data.Year = film.Year;

                    db.SaveChanges();

                    flag.FilmCode = data.FilmCode;
                    flag.Genre = data.Genre;
                    flag.Image = data.Image;
                    flag.Name = data.Name;
                    flag.Year = data.Year;
                }                
            }
            catch(Exception e)
            {
                flag = null;
            }

            return flag;
        }
    }
}