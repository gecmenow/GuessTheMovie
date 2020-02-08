using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.DataBase
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext() : base("GuessTheMovie_Connection")
        { }

        public DbSet<FilmsDB> FilmsDB { get; set; }
        public DbSet<AdminDB> AdminDB { get; set; }
    }
}