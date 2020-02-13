using GuessTheMovie.Models.DataBase;
using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GuessTheMovie.Models.MovieDataBase
{
    public class FilmInfo
    {
        public static async Task<string> GetFilmGenres(string filmCode)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync("https://api.themoviedb.org/3/movie/"+ filmCode + "?api_key=7bf0ddbd0b708dd904d550607793fa52&language=ru-RU");

            FilmGenresVM filmGenres = null;

            try
            {
                filmGenres = FilmGenresVM.FromJson(response);
            }
            catch(Exception e)
            {

            }

            List<string> genres = new List<string>();

            if (filmGenres != null)
                foreach (var genre in filmGenres.Genres)
                    genres.Add(genre.Name);

            string filmGenre = genres.FirstOrDefault();

            return filmGenre;
        }

        public static async Task<string> GetFilmImages(string filmCode)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync("https://api.themoviedb.org/3/movie/" + filmCode + "/images?api_key=7bf0ddbd0b708dd904d550607793fa52");

            FilmImagesVM filmImages = null;

            try
            {
                filmImages = FilmImagesVM.FromJson(response);
            }
            catch (Exception e)
            {

            }

            List<string> images = new List<string>();

            string link = "https://image.tmdb.org/t/p/original";

            string filmImage = "";

            if(filmImages != null)
                foreach (var image in filmImages.Backdrops)
                    filmImage += link + image.FilePath + ";";
                //images.Add(link + image.FilePath);

            return filmImage;
        }

        public static async Task<string> GetSimilarFilms(string filmCode)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync("https://api.themoviedb.org/3/movie/" + filmCode + "/similar?api_key=7bf0ddbd0b708dd904d550607793fa52&language=ru-RU");

            List<Result> similar = null;

            try 
            {
                similar = FilmSimilarVM.FromJson(response).SimResults.ToList();
            }
            catch(Exception e)
            { }

            string similarFilms = null;

            if(similar != null)
                if (similar.Count() >= 4)
                {
                    foreach (var film in similar)
                        similarFilms += film.Id.ToString() + ";";
                }

            return similarFilms;
        }

        public static void CheckSimilarFilms()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var films = db.FilmsDB.ToList();

                foreach (var film in films)
                {
                    List<string> filmsCode = film.SimilarFilmsCode.Split(';').ToList();

                    foreach (var data in filmsCode)
                        if (films.Any(x => x.FilmCode != data))
                            filmsCode.Remove(data);

                    string similarFilms = null;

                    foreach (var fCode in filmsCode)
                        similarFilms += film.Id.ToString() + ";";

                    film.FilmCode = similarFilms;
                }
                    
            }
        }
    }
}