using Microsoft.EntityFrameworkCore;

namespace weatherapp.Models
{
    public class SearchHistoryContext : DbContext
    {
        public SearchHistoryContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<SearchHistoryModel> SearchHistoryModels { get; set; }
    }
}
