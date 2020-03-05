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

            int maxPage = 500;

            HttpClient client = new HttpClient();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            int page = 15;

            while (page != maxPage)
            {
                var response = await client.GetStringAsync("https://api.themoviedb.org/3/movie/popular?api_key=" + key + "&language=ru-RU&page=" + page.ToString());

                TmdBv1 tmdbV1 = TmdBv1.FromJson(response);

                List<string> images = new List<string>();

                using (DataBaseContext db = new DataBaseContext())
                {
                    foreach (var film in tmdbV1.Results)
                    {
                        FilmsDB data = new FilmsDB();

                        data.FilmCode = film.Id.ToString();

                        var filmId = db.FilmsDB.Select(f => f.FilmCode).ToList();

                        if (!filmId.Contains(data.FilmCode))
                        {
                            data.Name = film.Title;

                            data.Image = await FilmInfo.GetFilmImages(data.FilmCode);

                            if (data.Image == "")
                                continue;

                            data.SimilarFilmsCode = await FilmInfo.GetSimilarFilms(data.FilmCode);

                            if (data.SimilarFilmsCode == null)
                                continue;

                            data.Genre = await FilmInfo.GetFilmGenres(data.FilmCode);

                            if (data.Genre == null)
                                continue;

                            data.Year = film.ReleaseDate.Year;

                            db.FilmsDB.Add(data);

                            db.SaveChanges();
                        }

                        //List<string> genres = await FilmInfo.GetFilmGenres(data.FilmCode);
                    }

                    page++;

                    maxPage = Convert.ToInt32(tmdbV1.TotalPages / 10);
                }
            }
        }

        public static AdminFilmsVM GetFilm(int id)
        {
            AdminFilmsVM data = new AdminFilmsVM();

            using (DataBaseContext db = new DataBaseContext())
            {
                data = db.FilmsDB.Where(f => f.Id == id).Select(f =>
                  new AdminFilmsVM
                  {
                      FilmCode = f.FilmCode,
                      Genre = f.Genre,
                      Image = f.Image,
                      Name = f.Name,
                      Year = f.Year,
                      SimilarFilmsCode = f.SimilarFilmsCode,
                  }).FirstOrDefault();

                data.FilmImages = data.Image.Split(';').ToList();

                data.FilmImages.RemoveAt(data.FilmImages.Count() - 1);
            }

            return data;
        }

        public static List<AdminFilmsVM> GetFilms()
        {
            List<AdminFilmsVM> data = new List<AdminFilmsVM>();

            using(DataBaseContext db = new DataBaseContext())
            {
                data = db.FilmsDB.Select(f =>
                new AdminFilmsVM
                {
                    FilmId = f.Id,
                    FilmCode = f.FilmCode,
                    Genre = f.Genre,
                    Image = f.Image,
                    Name = f.Name,
                    Year = f.Year,
                }).ToList();

                foreach (var film in data)
                    film.ImageCount = film.Image.Split(';').Count();
            }

            return data;
        }

        public static AdminFilmsVM EditFilm(AdminFilmsVM film)
        {
            AdminFilmsVM flag = new AdminFilmsVM();

            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var data = db.FilmsDB.Where(f => f.FilmCode == film.FilmCode).FirstOrDefault();

                    data.FilmCode = film.FilmCode;
                    data.Genre = film.Genre;
                    data.Image = String.Join(";", film.ImageList.ToArray());
                    data.Name = film.Name;
                    data.Year = film.Year;

                    db.SaveChanges();

                    flag.FilmCode = data.FilmCode;
                    flag.Genre = data.Genre;
                    flag.FilmImages = data.Image.Split(';').ToList();
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

        public static void DeleteFilm(int id)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var data = db.FilmsDB.Where(f => f.Id == id).FirstOrDefault();

                db.FilmsDB.Remove(data);

                db.SaveChanges();
            }
        }

        public static void ClearDataBase()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var data = db.FilmsDB.ToList();

                foreach (var film in data)
                    db.FilmsDB.Remove(film);

                db.SaveChanges();
            }
        }
    }
}