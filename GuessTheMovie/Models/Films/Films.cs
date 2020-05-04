﻿using GuessTheMovie.Models.DataBase;
using GuessTheMovie.Models.MovieDataBase;
using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GuessTheMovie.Models.Films
{
    public class Films
    {
        public static async Task<List<FilmVM>> GetFilms(string years, string genres)
        {
            //Преобразвание строки годы в список
            List<int> yearList = new List<int>();

            if (years != null)
            {
                var temp = years.Split(';').ToList();

                foreach (var year in temp)
                    yearList.Add(Convert.ToInt32(year));
            }
            //

            //Преобразвание строки жанры в список
            List<string> genreList = new List<string>();

            if (genres != null)
            {
                var temp = genres.Split(';').ToList();

                foreach (var genre in temp)
                    genreList.Add(genre);
            }
            //

            FilmVM data = null;

            while (data == null)
                data = await RandomFilms.GetFirstRandomFilm(yearList, genreList);

            List<FilmVM> films = await FilmsPool.GetPool(data, yearList, genreList);

            foreach (var film in films)
            {
                if (film.FilmImage != null)
                    film.FilmImage = film.FilmImage.Split(';')[0];
            }

            films = MixFilms(films);

            return films;
        }

        //public static async Task<List<FilmVM>> GetFilms(string watched)
        //{
        //    List<string> watchedList = new List<string>();

        //    FilmVM data = null;

        //    while (data == null)
        //        data = await GetFirstRandomFilm();

        //    if (watched != null)
        //    {
        //        watchedList = watched.Split(';').ToList();

        //        watchedList.RemoveAt(watchedList.Count - 1);

        //        while (true)
        //        {
        //            if (!watchedList.Contains(data.FilmCode))
        //                break;
        //            else
        //                data = await GetFirstRandomFilm();
        //        }
        //    }

        //    List<FilmVM> films = await GetPool(data);

        //    foreach (var film in films)
        //        if (film.FilmImage != null)
        //            film.FilmImage = film.FilmImage.Split(';')[0];

        //    films = MixFilms(films);

        //    return films;
        //}

        /*
         Получение списка фильмов, учитывая просмотренные и фильтры
             */
        public static async Task<List<FilmVM>> GetFilms(string watched, string years, string genres)
        {
            //Преобразвание строки годы в список
            List<int> yearList = new List<int>();

            if (years != null)
            {
                var temp = years.Split(';').ToList();

                foreach (var year in temp)
                    yearList.Add(Convert.ToInt32(year));
            }
            //

            //Преобразвание строки жанры в список
            List<string> genreList = new List<string>();

            if (genres != null)
            {
                var temp = genres.Split(';').ToList();

                foreach (var genre in temp)
                    genreList.Add(genre);
            }
            //

            FilmVM data = null;

            data = await RandomFilms.GetFirstRandomFilm(yearList, genreList);

            List<string> watchedList = new List<string>();

            if (watched != null)
            {
                watchedList = watched.Split(';').ToList();

                watchedList.RemoveAt(watchedList.Count - 1);

                while (true)
                {
                    if (!watchedList.Contains(data.FilmCode))
                        break;
                    else
                        data = await RandomFilms.GetFirstRandomFilm(yearList, genreList);
                }
            }

            List<FilmVM> films = await FilmsPool.GetPool(data, yearList, genreList);

            foreach (var film in films)
                if (film.FilmImage != null)
                    film.FilmImage = film.FilmImage.Split(';')[0];

            films = MixFilms(films);

            return films;
        }

        //public static async Task<FilmVM> GetFirstRandomFilm()
        //{
        //    FilmVM data = new FilmVM();

        //    Random random = new Random();

        //    using (DataBaseContext db = new DataBaseContext())
        //    {
        //        var filmsCount = db.FilmsDB.Count();
        //        var startIndex = db.FilmsDB.FirstOrDefault().Id;
        //        var lastIndex = db.FilmsDB.ToList().LastOrDefault().Id;

        //        int index = random.Next(startIndex, lastIndex);

        //        data = await db.FilmsDB.Where(f => f.Id == index).Select(
        //            x => new FilmVM
        //            {
        //                FilmCode = x.FilmCode,
        //                FilmName = x.Name,
        //                FilmYear = x.Year,
        //                FilmGenre = x.Genre,
        //                FilmImage = x.Image,
        //                Correct = true,
        //            }).FirstOrDefaultAsync();
        //    }

        //    return data;
        //}



        //public static async Task<FilmVM> GetRandomFilm()
        //{
        //    FilmVM data = new FilmVM();

        //    Random random = new Random();

        //    using (DataBaseContext db = new DataBaseContext())
        //    {
        //        var filmsCount = db.FilmsDB.Count();
        //        var startIndex = db.FilmsDB.FirstOrDefault().Id;
        //        var lastIndex = db.FilmsDB.ToList().LastOrDefault().Id;

        //        int index = random.Next(startIndex, lastIndex);

        //        data = await db.FilmsDB.Where(f => f.Id == index).Select(
        //            x => new FilmVM
        //            {
        //                FilmCode = x.FilmCode,
        //                FilmName = x.Name,
        //                FilmYear = x.Year,
        //                FilmGenre = x.Genre,
        //                FilmImage = x.Image,
        //                Correct = true,
        //            }).FirstOrDefaultAsync();
        //    }

        //    return data;
        //}
        /*
         Получение случайного фильма в заданных пределах
             */


        public static List<FilmVM> MixFilms(List<FilmVM> films)
        {
            Random random = new Random();

            for (int i = films.Count() - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                // обменять значения data[j] и data[i]
                var temp = films[j];
                films[j] = films[i];
                films[i] = temp;
            }

            return films;
        }

        //public static async Task<List<FilmVM>> GetPool(FilmVM data)
        //{
        //    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

        //    //HttpClient client = new HttpClient();

        //    //var response = await client.GetStringAsync("https://api.themoviedb.org/3/movie/" + data.FilmCode + "/similar?api_key=7bf0ddbd0b708dd904d550607793fa52&language=ru-RU");

        //    //var response = await client.GetStringAsync("https://api.themoviedb.org/3/movie/" + 466272 + "/similar?api_key=7bf0ddbd0b708dd904d550607793fa52&language=ru-RU");

        //    List<FilmVM> similarFilms = new List<FilmVM>();

        //    using (DataBaseContext db = new DataBaseContext())
        //    {
        //        var filmInfo = db.FilmsDB.Where(f => f.FilmCode == data.FilmCode).Select(
        //            f => new FilmsVM
        //            {
        //                SimilarFilmsCode = f.SimilarFilmsCode,
        //            }).FirstOrDefault();

        //        var similarFilmsList = filmInfo.SimilarFilmsCode.Split(';').ToList();

        //        foreach (var simFilms in similarFilmsList)
        //        {
        //            try
        //            {
        //                var similar = db.FilmsDB.Where(f => f.FilmCode == simFilms).Select(f => new FilmVM
        //                {
        //                    FilmCode = f.FilmCode,
        //                    FilmName = f.Name,
        //                    FilmYear = f.Year,
        //                    FilmGenre = f.Genre,
        //                }).FirstOrDefault();


        //                similarFilms.Add(similar);
        //            }
        //            catch(Exception e)
        //            { }

        //        }
        //    }

        //    var temp = similarFilms.ToList();

        //    foreach (var similar in similarFilms)
        //        if (similar == null)
        //            temp.Remove(similar);

        //    similarFilms.Clear();

        //    similarFilms = temp.ToList();

        //    while (similarFilms.Count() < 6)
        //    {
        //        FilmVM film = null;

        //        while (film == null)
        //            film = await GetRandomFilm();

        //        similarFilms.Add(film);
        //    }

        //    List<FilmVM> films = new List<FilmVM>();

        //    films.Add(data);

        //    Random random = new Random();

        //    while (films.Count() < 4)
        //    {
        //        int index = random.Next(similarFilms.Count());

        //        if (films.Any(x=>x.FilmCode == similarFilms[index].FilmCode) == false)
        //            films.Add(similarFilms[index]);
        //    }

        //    return films;
        //}
    }
}