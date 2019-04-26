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
        public void Add(SearchHistoryModel entity)
        {
            context.SearchHistoryModels.Add(entity);
            context.SaveChanges();
        }
    }
}
