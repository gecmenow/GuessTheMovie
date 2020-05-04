using GuessTheMovie.Models.DataBase;
using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GuessTheMovie.Models.Films
{
    public class FilmsPool
    {
        public static async Task<List<FilmVM>> GetPool(FilmVM data, List<int> years, List<string> genres)
        {
            List<FilmVM> similarFilms = new List<FilmVM>();

            using (DataBaseContext db = new DataBaseContext())
            {
                var filmInfo = db.FilmsDB.Where(f => f.FilmCode == data.FilmCode).Select(
                    f => new FilmsVM
                    {
                        SimilarFilmsCode = f.SimilarFilmsCode,
                    }).FirstOrDefault();

                var similarFilmsList = filmInfo.SimilarFilmsCode.Split(';').ToList();

                foreach (var simFilms in similarFilmsList)
                {
                    try
                    {
                        var similar = db.FilmsDB.Where(f => f.FilmCode == simFilms).Select(f => new FilmVM
                        {
                            FilmCode = f.FilmCode,
                            FilmName = f.Name,
                            FilmYear = f.Year,
                            FilmGenre = f.Genre,
                            FilmImage = f.Image,
                            SimilarFilmsCode = f.SimilarFilmsCode,
                        }).FirstOrDefault();

                        if (years.Contains(similar.FilmYear) || genres.Contains(similar.FilmGenre))
                            if (similarFilms.Count() != 0)
                            {
                                if (!similarFilms.Contains(similar))
                                    similarFilms.Add(similar);
                            }
                            else
                                similarFilms.Add(similar);

                    }
                    catch (Exception e)
                    { }

                }
            }

            var temp = similarFilms.ToList();

            foreach (var similar in similarFilms)
                if (similar == null)
                    temp.Remove(similar);

            similarFilms.Clear();

            similarFilms = temp.ToList();

            while (similarFilms.Count() < 6)
            {
                FilmVM film = null;

                while (film == null)
                    film = await RandomFilms.GetRandomFilm(years, genres);

                if (similarFilms.Count() != 0)
                {
                    //var flag = similarFilms.Contains(film);

                    bool flag = false;

                    foreach (var simFilm in similarFilms)
                    {
                        if (simFilm.FilmCode == film.FilmCode)
                            flag = true;
                    }

                    if (flag == false)
                        similarFilms.Add(film);
                }
                else
                    similarFilms.Add(film);

            }

            List<FilmVM> films = new List<FilmVM>();

            films.Add(data);

            Random random = new Random();

            while (films.Count() < 4)
            {
                int index = random.Next(similarFilms.Count());

                if (films.Any(x => x.FilmCode == similarFilms[index].FilmCode) == false)
                    films.Add(similarFilms[index]);
            }

            return films;
        }
    }
}