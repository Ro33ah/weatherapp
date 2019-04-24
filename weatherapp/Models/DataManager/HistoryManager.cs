using System.Collections.Generic;
using System.Linq;
using weatherapp.Models.Repository;

namespace weatherapp.Models.DataManager
{
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
