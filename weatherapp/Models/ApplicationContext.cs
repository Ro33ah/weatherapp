using Microsoft.EntityFrameworkCore;

namespace weatherapp.Models

{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
    }
}
