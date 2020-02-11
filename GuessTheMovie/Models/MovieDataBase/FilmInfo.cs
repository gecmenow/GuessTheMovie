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

            FilmGenresVM filmGenres = FilmGenresVM.FromJson(response);

            List<string> genres = new List<string>();

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

            FilmImagesVM filmImages = FilmImagesVM.FromJson(response);

            List<string> images = new List<string>();

            string link = "https://image.tmdb.org/t/p/original";

            foreach (var image in filmImages.Backdrops)
                images.Add(link + image.FilePath);

            string filmImage = images.FirstOrDefault();

            return filmImage;            
        }
    }
}