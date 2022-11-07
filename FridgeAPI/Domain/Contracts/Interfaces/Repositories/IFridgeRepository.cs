using FridgeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeAPI.Domain.Contracts.Interfaces.Repositories
{
    public interface IFridgeRepository
    {
        Task<IEnumerable<Fridge>> GetFridgesAsync();

        Task<Fridge> GetFridgeAsync(Guid id);

        Task<Fridge> AddFridgeAsync(Fridge fridge);

        Task DeleteFridgeAsync(Guid id);

        Task<int> SaveAsync();
    }
}
