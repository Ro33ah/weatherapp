using System.Collections.Generic;


namespace weatherapp.Models.Repository
{
    interface IHistoryRepository<TEntity>
    {
        IEnumerable<TEntity> GetHistoryAll();
        void Add(TEntity entity);

    }
}
