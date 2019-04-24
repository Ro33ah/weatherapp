using System.Collections.Generic;


namespace weatherapp.Models.Repository
{
    interface IHistoryRepository<TEntity>
    {
        IEnumerable<TEntity> GetHistoryAll();
        void Add(TEntity entity);

    }

    public class HistoryManager : IHistoryRepository<WeatherModel>
    {
        ApplicationContext context;
        public HistoryManager(ApplicationContext _context)
        {
            context = _context;
        }
        public IEnumerable<WeatherModel> GetHistoryAll()
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
