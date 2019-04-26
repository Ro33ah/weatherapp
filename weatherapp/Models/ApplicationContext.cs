using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace weatherapp.Models

{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<WeatherModel> WeatherModels { get; set; }
        public DbSet<Reading> Readings { get; set; }
        public DbSet<Main> Mains { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
