using FridgeAPI.Domain.Contracts.Exceptions;
using FridgeAPI.Domain.Contracts.Interfaces.Repositories;
using FridgeAPI.Domain.Entities;
using FridgeAPI.Infrastructure.Database.Misc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeAPI.Infrastructure.Database.Repositories
{
    public class FridgeRepository : RepositoryBase, IFridgeRepository
    {
        public FridgeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<Fridge> AddFridgeAsync(Fridge fridge)
        {
            var entity = await _dbContext.Fridges.AddAsync(fridge);
            return entity.Entity;
        }

        public async Task DeleteFridgeAsync(Guid id)
        {
            var entity = await _dbContext.Fridges.FirstOrDefaultAsync(f => f.Id == id);

            if (entity != null)
                _dbContext.Fridges.Remove(entity);
        }

        public async Task<Fridge> GetFridgeAsync(Guid id)
        {
            try
            {
                var fridge = await _dbContext.Fridges
                    .Include(f => f.Model)
                    .Include(f => f.Products).ThenInclude(p => p.Product)
                    .SingleAsync(f => f.Id == id);

                return fridge;
            }
            catch (Exception)
            {
                throw new EntityNotFoundException(id, $"Fridge with id {id} not found");
            }
        }

        public async Task<IEnumerable<Fridge>> GetFridgesAsync()
        {
            return await _dbContext.Fridges
                .Include(f => f.Model)
                .Include(f => f.Products).ThenInclude(p => p.Product)
                .ToListAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
