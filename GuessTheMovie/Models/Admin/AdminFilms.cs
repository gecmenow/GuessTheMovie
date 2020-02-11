using GuessTheMovie.Models.DataBase;
using GuessTheMovie.Models.MovieDataBase;
using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GuessTheMovie.Models.Admin
{
    public class AdminFilms
    {
        public static async Task UpdateDatabase()
        {
            string key = "7bf0ddbd0b708dd904d550607793fa52";
            string page = "1";

            HttpClient client = new HttpClient();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            var response = await client.GetStringAsync("https://api.themoviedb.org/3/movie/popular?api_key=" + key + "&language=ru-RU&page=" + page);

            TmdBv1 tmdbV1 = TmdBv1.FromJson(response);

            List<string> images = new List<string>();

            using(DataBaseContext db = new DataBaseContext())
            {
                foreach (var film in tmdbV1.Results)
                {
                    FilmsDB data = new FilmsDB();

                    data.FilmCode = film.Id.ToString();

                    var filmId = db.FilmsDB.Select(f => f.FilmCode).ToList();

                    if (!filmId.Contains(data.FilmCode))
                    {
                        data.Genre = await FilmInfo.GetFilmGenres(data.FilmCode);

                        data.Name = film.Title;

                        data.Image = await FilmInfo.GetFilmImages(data.FilmCode);

                        data.Year = film.ReleaseDate.Year;

                        db.FilmsDB.Add(data);

                        db.SaveChanges();
                    }

                    //List<string> genres = await FilmInfo.GetFilmGenres(data.FilmCode);

                    string temp = "";
                }
            } 
        }

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