using GuessTheMovie.Models.Films;
using GuessTheMovie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GuessTheMovie.Controllers
{
    public class FilmsController : ApiController
    {

        [System.Web.Http.HttpGet]
        public async Task<IEnumerable<PoolVM>> Get()
        {
            //async
            var data = await Films.GetPool();
            //var data = db.FilmsDB.ToListAsync();      

            return data;
        }
    }
}
