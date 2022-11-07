using FridgeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeAPI.Domain.Contracts.Interfaces.Repositories
{
    public interface IFridgeModelRepository
    {
        Task<IEnumerable<FridgeModel>> GetFridgeModelsAsync();

        Task<FridgeModel> GetFridgeModelAsync(Guid id);
    }
}
