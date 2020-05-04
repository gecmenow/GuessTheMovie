using GuessTheMovie.Cors;
using GuessTheMovie.Models.Films;
using GuessTheMovie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GuessTheMovie.Controllers
{
    public class FilmsController : ApiController
    {
        public async Task<IEnumerable<FilmVM>> Get(string years, string genres)
        {
            if (years == "null")
                years = null;
            if (genres == "null")
                genres = null;
            //async
            var data = await Films.GetFilms(years, genres);

            return data;
        }

        public async Task<IEnumerable<FilmVM>> Get(string id, string years, string genres)
        {
            if (years == "null")
                years = null;
            if (genres == "null")
                genres = null;
            //async
            var data = await Films.GetFilms(id, years, genres);

            return data;
        }
    }
}
