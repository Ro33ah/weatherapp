using System.Collections.Generic;
using System.Linq;
using weatherapp.Models;

namespace weatherapp.Repository
{
    public interface ISearchHistoryRepository<TEntity>
    {
        IEnumerable<TEntity> GetSearchHistoryAll();
        void Add(TEntity entity);

    }

    public class SearchHistoryManager : ISearchHistoryRepository<WeatherModel>
    {
        ApplicationContext context;
        public SearchHistoryManager(ApplicationContext _context)
        {
            context = _context;
        }
        public IEnumerable<WeatherModel> GetSearchHistoryAll()
        {
            return context.WeatherModels.ToList();

        }
        public void Add(WeatherModel entity)
        {
            context.WeatherModels.Add(entity);
            context.SaveChanges();
        }
    }
}
