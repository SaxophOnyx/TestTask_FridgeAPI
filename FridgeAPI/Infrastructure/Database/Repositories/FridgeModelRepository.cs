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
    public class FridgeModelRepository : RepositoryBase, IFridgeModelRepository
    {
        public FridgeModelRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {

        }


        public async Task<FridgeModel> GetFridgeModelAsync(Guid id)
        {
            try
            {
                return await _dbContext.FridgeModels.SingleAsync(x => x.Id == id);
            }
            catch
            {
                throw new EntityNotFoundException(id, $"Fridge model with id {id} not found");
            }
        }

        public async Task<IEnumerable<FridgeModel>> GetFridgeModelsAsync()
        {
            return await _dbContext.FridgeModels.ToListAsync();
        }
    }
}
