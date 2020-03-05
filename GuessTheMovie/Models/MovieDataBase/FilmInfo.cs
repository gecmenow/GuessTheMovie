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

            string url = "https://api.themoviedb.org/3/movie/" + filmCode + "?api_key=7bf0ddbd0b708dd904d550607793fa52&language=ru-RU";

            HttpResponseMessage responseRequest = await client.GetAsync(url);

            FilmGenresVM filmGenres = null;

            string filmGenre = "";

            if (responseRequest.IsSuccessStatusCode)
            {
                var response = await client.GetStringAsync(url);

                try
                {
                    filmGenres = Newtonsoft.Json.JsonConvert.DeserializeObject<FilmGenresVM>(response);
                }
                catch (Exception e)
                {

                }

                List<string> genres = new List<string>();

                if (filmGenres != null)
                    foreach (var genre in filmGenres.genres)
                        filmGenre += genre + ";";

                //filmGenre = genres.FirstOrDefault();
            }

            return filmGenre;
        }

        public static async Task<string> GetFilmImages(string filmCode)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpClient client = new HttpClient();

            string url = "https://api.themoviedb.org/3/movie/" + filmCode + "/images?api_key=7bf0ddbd0b708dd904d550607793fa52";

            HttpResponseMessage responseRequest = await client.GetAsync(url);

            string filmImage = "";

            if (responseRequest.IsSuccessStatusCode)
            {
                var response = await client.GetStringAsync(url);

                FilmImagesVM filmImages = null;

                try
                {
                    //filmImages = FilmImagesVM.FromJson(response);
                    filmImages = Newtonsoft.Json.JsonConvert.DeserializeObject<FilmImagesVM>(response);
                }
                catch (Exception e)
                {

                }

                List<string> images = new List<string>();

                string link = "https://image.tmdb.org/t/p/original";

                if (filmImages != null)
                    foreach (var image in filmImages.backdrops)
                        filmImage += link + image.file_path + ";";
                //images.Add(link + image.FilePath);
            }

            return filmImage;
        }

        public static async Task<string> GetSimilarFilms(string filmCode)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpClient client = new HttpClient();

            string url = "https://api.themoviedb.org/3/movie/" + filmCode + "/similar?api_key=7bf0ddbd0b708dd904d550607793fa52&language=ru-RU";

            HttpResponseMessage responseRequest = await client.GetAsync(url);

            string similarFilms = null;

            if (responseRequest.IsSuccessStatusCode)
            {
                var response = await client.GetStringAsync(url);

                FilmSimilarVM similar = null;
                try
                {
                    similar = Newtonsoft.Json.JsonConvert.DeserializeObject<FilmSimilarVM>(response);
                }
                catch (Exception e)
                { }

                if (similar != null)
                {
                    if (similar.results.Count() >= 4)
                    {
                        foreach (var film in similar.results)
                            similarFilms += film.id.ToString() + ";";
                    }
                }
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