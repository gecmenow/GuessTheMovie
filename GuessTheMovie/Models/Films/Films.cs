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
    public class Films
    {
        public static async Task<List<PoolVM>> GetPool()
        {
            List<PoolVM> data = new List<PoolVM>();

            using (DataBaseContext db = new DataBaseContext())
            {
                data = await db.FilmsDB.Select(
                    x => new PoolVM
                    {
                        FilmCode = x.FilmCode,
                        FilmName = x.Name,
                        FilmYear = x.Year,
                        FilmGenre = x.Genre,
                        FilmImage = x.Image,
                    }).ToListAsync();
            }

            List<PoolVM> films = SortFilms.Sort(data);

            return films;
        }
    }
}