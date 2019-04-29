using System;
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
        readonly SearchHistoryContext context;
        public SearchHistoryManager(SearchHistoryContext _context)
        {
            context = _context;
        }
        public IEnumerable<SearchHistoryModel> GetSearchHistoryAll()
        {
            return context.SearchHistoryModels.ToList();

        }
        public void UpdateHistory(SearchHistoryModel entity)
        {
            if ((context.SearchHistoryModels.Where(x=>x.CityName == entity.CityName).Count()) == 0)
            {
                context.SearchHistoryModels.Add(entity);
                context.SaveChanges();
            }
        }
    }
}
