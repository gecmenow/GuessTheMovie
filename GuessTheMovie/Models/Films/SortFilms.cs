﻿using GuessTheMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.Films
{
    public class SortFilms
    {
        static List<string> watched = new List<string>();

        public static List<PoolVM> Sort(List<PoolVM> data)
        {
            List<PoolVM> films = new List<PoolVM>();

            Random r = new Random();

            int index = 0;

            while (films.Count() < 1)
            {
                int moviesCount = data.Count();

                index = r.Next(moviesCount);

                if (watched.Count() > 0)
                {
                    if (!watched.Contains(data[index].FilmCode))
                    {
                        watched.Add(data[index].FilmCode);

                        films.Add(data[index]);
                    }
                }

                else if (watched.Count() >= moviesCount)
                    break;

                else
                {
                    watched.Add(data[index].FilmCode);

                    films.Add(data[index]);
                }
            }

            films.First().Correct = true;

            string genre = films.Select(x => x.FilmGenre).FirstOrDefault();

            var genrePool = data.Where(x => x.FilmGenre == genre).ToList();

            while (films.Count() < 4)
            {
                index = r.Next(genrePool.Count());

                if (!films.Contains(genrePool[index]))
                    films.Add(genrePool[index]);
            }

            var temp = films.ToList();

            films.Clear();

            while (temp.Count() != films.Count())
            {
                index = r.Next(temp.Count());

                if (films.Count() > 0)
                {
                    if (!films.Contains(temp[index]))
                        films.Add(temp[index]);
                }
                else
                    films.Add(temp[index]);
            }

            return films;
        }
    }
}