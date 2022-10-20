using System;
using System.Linq;

namespace FridgeAPI.Entities.DatabaseAccess.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        T GetEntity(Guid id);

        IQueryable<T> GetEntities();

        void AddEntity(T entity);

        void UpdateEntity(T entity);

        void DeleteEntity(T entity);

        int Save();
    }
}
