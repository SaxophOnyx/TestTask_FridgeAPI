using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using FridgeAPI.Entities.DatabaseAccess.Abstractions;

namespace FridgeAPI.Entities.DatabaseAccess.Implmentations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly DbContext _context;


        public GenericRepository(DbContext context)
        {
            _context = context;
        }


        public IQueryable<T> GetEntities()
        {
            return _context.Set<T>().AsQueryable();
        }

        public T GetEntity(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
