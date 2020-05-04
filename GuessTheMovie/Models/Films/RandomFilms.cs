using GuessTheMovie.Models.DataBase;
using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GuessTheMovie.Models.Films
{
    public class RandomFilms
    {
        public static async Task<FilmVM> GetFirstRandomFilm(List<int> years, List<string> genres)
        {
            FilmVM data = new FilmVM();

            using (DataBaseContext db = new DataBaseContext())
            {
                List<FilmsDB> films = new List<FilmsDB>();

                if (years.Count() != 0)
                {
                    foreach (var year in years)
                    {
                        var filmList = await db.FilmsDB.Where(f => f.Year == year).ToListAsync();

                        foreach (var film in filmList)
                            films.Add(film);
                    }
                }

                if (genres.Count() != 0)
                {
                    foreach (var genre in genres)
                    {
                        var filmList = await db.FilmsDB.Where(f => f.Genre == genre).ToListAsync();

                        foreach (var film in filmList)
                        {
                            if (films.Count() != 0)
                            {
                                if (!films.Contains(film))
                                    films.Add(film);
                            }
                            else
                            {
                                films.Add(film);
                            }
                        }
                    }
                }

                Random random = new Random();

                if (genres.Count() == 0 && years.Count() == 0)
                {
                    var filmsCount = db.FilmsDB.Count();
                    var startIndex = db.FilmsDB.FirstOrDefault().Id;
                    var lastIndex = db.FilmsDB.ToList().LastOrDefault().Id;

                    int index = random.Next(startIndex, lastIndex);

                    data = await db.FilmsDB.Where(f => f.Id == index).Select(
                        x => new FilmVM
                        {
                            FilmCode = x.FilmCode,
                            FilmName = x.Name,
                            FilmYear = x.Year,
                            FilmGenre = x.Genre,
                            FilmImage = x.Image,
                            Correct = true,
                        }).FirstOrDefaultAsync();
                }
                else
                {
                    int index = random.Next(0, films.Count() - 1);

                    data = new FilmVM
                    {
                        FilmCode = films[index].FilmCode,
                        FilmName = films[index].Name,
                        FilmYear = films[index].Year,
                        FilmGenre = films[index].Genre,
                        FilmImage = films[index].Image,
                        SimilarFilmsCode = films[index].SimilarFilmsCode,
                        Correct = true,
                    };
                }
            }

            return data;
        }

        public static async Task<FilmVM> GetRandomFilm(List<int> years, List<string> genres)
        {
            FilmVM data = new FilmVM();

            using (DataBaseContext db = new DataBaseContext())
            {
                List<FilmsDB> films = new List<FilmsDB>();

                if (years.Count() != 0)
                {
                    foreach (var year in years)
                    {
                        var filmList = await db.FilmsDB.Where(f => f.Year == year).ToListAsync();

                        foreach (var film in filmList)
                        {
                            if (films.Count() != 0)
                            {
                                if (!films.Contains(film))
                                    films.Add(film);
                            }
                            else
                            {
                                films.Add(film);
                            }
                        }
                    }
                }

                if (genres.Count() != 0)
                {
                    foreach (var genre in genres)
                    {
                        var filmList = await db.FilmsDB.Where(f => f.Genre == genre).ToListAsync();

                        foreach (var film in filmList)
                        {
                            if (films.Count() != 0)
                            {
                                if (!films.Contains(film))
                                    films.Add(film);
                            }
                            else
                            {
                                films.Add(film);
                            }
                        }
                    }
                }

                Random random = new Random();

                if (genres.Count() == 0 && years.Count() == 0)
                {
                    var filmsCount = db.FilmsDB.Count();
                    var startIndex = db.FilmsDB.FirstOrDefault().Id;
                    var lastIndex = db.FilmsDB.ToList().LastOrDefault().Id;

                    int index = random.Next(startIndex, lastIndex);

                    data = await db.FilmsDB.Where(f => f.Id == index).Select(
                        x => new FilmVM
                        {
                            FilmCode = x.FilmCode,
                            FilmName = x.Name,
                            FilmYear = x.Year,
                            FilmGenre = x.Genre,
                            FilmImage = x.Image,
                            SimilarFilmsCode = x.SimilarFilmsCode,
                        }).FirstOrDefaultAsync();
                }
                else
                {
                    int index = random.Next(0, films.Count() - 1);

                    data = new FilmVM
                    {
                        FilmCode = films[index].FilmCode,
                        FilmName = films[index].Name,
                        FilmYear = films[index].Year,
                        FilmGenre = films[index].Genre,
                        FilmImage = films[index].Image,
                        SimilarFilmsCode = films[index].SimilarFilmsCode,
                    };
                }

                //startIndex = db.FilmsDB.FirstOrDefault().Id;
                //lastIndex = db.FilmsDB.ToList().LastOrDefault().Id;

                //while (data == null)
                //{
                //    int index = random.Next(startIndex, lastIndex);

                //    data = await db.FilmsDB.Where(f => f.Id == index).Select(
                //        x => new FilmVM
                //        {
                //            FilmCode = x.FilmCode,
                //            FilmName = x.Name,
                //            FilmYear = x.Year,
                //            FilmGenre = x.Genre,
                //            FilmImage = x.Image,
                //            Correct = true,
                //        }).FirstOrDefaultAsync();

                //    if (!years.Contains(data.FilmYear))
                //        data = null;
                //}
            }

            return data;
        }
    }
}