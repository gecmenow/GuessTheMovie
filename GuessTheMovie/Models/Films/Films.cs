using GuessTheMovie.Models.DataBase;
using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
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

            Random random = new Random();

            using (DataBaseContext db = new DataBaseContext())
            {
                var count = db.FilmsDB.Count();

                var startIndex = random.Next(0, count - 100);

                var endIndex = random.Next(startIndex + 50, count);

                data = await db.FilmsDB.Where(f => f.Id >= startIndex && f.Id <= endIndex).Select(
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