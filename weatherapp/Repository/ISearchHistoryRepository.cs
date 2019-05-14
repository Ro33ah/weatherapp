using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using weatherapp.Models;

namespace weatherapp.Repository
{
    public interface ISearchHistoryRepository<TEntity>
    {
        IEnumerable<TEntity> GetSearchHistoryAll();
        void UpdateHistory(TEntity entity);

    }

    public class SearchHistoryManager : ISearchHistoryRepository<SearchHistoryModel>
    {
        private readonly SearchHistoryContext context;
        
        private HistorySetting historySetting { get; set; }

        private readonly ILogger logger;

        public SearchHistoryManager(SearchHistoryContext _context, 
            IOptions<HistorySetting> _historySetting, 
            ILogger<SearchHistoryManager> _logger)
        {
            context = _context;
           historySetting = _historySetting.Value;
            logger = _logger;
        }
        public IEnumerable<SearchHistoryModel> GetSearchHistoryAll()
        {
            logger.Log(LogLevel.Information, "retrieving {HistoryLength} most recent search queries", historySetting.HistoryLength);
            return context.SearchHistoryModels.OrderByDescending(x => x.CityId).Take(historySetting.HistoryLength).ToList();

        }
        public void UpdateHistory(SearchHistoryModel entity)
        {
            if ((context.SearchHistoryModels.Where(x=>x.CityName == entity.CityName).Count()) == 0)
            {
                logger.Log(LogLevel.Information, "writing {entry} to database", entity.CityName);
                context.SearchHistoryModels.Add(entity);
                context.SaveChanges();
            }
            else
            {
                logger.Log(LogLevel.Information, "{entry} already exists in datababase", entity.CityName);
            }
        }
     
    }
}
