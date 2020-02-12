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
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IEnumerable<FilmVM>> Get()
        {
            Random r = new Random();

            //List<int> temp = new List<int>();

            //temp.Add(r.Next());

            //List<int> temp1 = new List<int>()
            //    temp1 = await (List<int>)HttpContext.Current.Session["name"];

            //HttpContext.Current.Session["name"] = temp;

            //async
            var data = await Films.GetFilms();
            //var data = db.FilmsDB.ToListAsync();      

            return data;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IEnumerable<FilmVM>> Get(string id)
        {
            Random r = new Random();

            //List<int> temp = new List<int>();

            //temp.Add(r.Next());

            //List<int> temp1 = new List<int>()
            //    temp1 = await (List<int>)HttpContext.Current.Session["name"];

            //HttpContext.Current.Session["name"] = temp;

            //async
            var data = await Films.GetFilms(id);
            //var data = db.FilmsDB.ToListAsync();      

            return data;
        }
    }
}
