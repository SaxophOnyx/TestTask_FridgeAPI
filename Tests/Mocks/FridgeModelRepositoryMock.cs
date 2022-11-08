using FridgeAPI.Domain.Contracts.Exceptions;
using FridgeAPI.Domain.Contracts.Interfaces.Repositories;
using FridgeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    public class FridgeModelRepositoryMock : IFridgeModelRepository
    {
        public List<FridgeModel> Entries { get; }


        public FridgeModelRepositoryMock(List<FridgeModel> models)
        {
            Entries = models;
        }


        public Task<IEnumerable<FridgeModel>> GetFridgeModelsAsync()
        {
            return Task.FromResult(Entries.AsEnumerable());
        }

        public Task<FridgeModel> GetFridgeModelAsync(Guid id)
        {
            try
            {
                return Task.FromResult(Entries.Single(m => m.Id == id));
            }
            catch
            {
                throw new EntityNotFoundException(id, $"Fridge model with id {id} not found");
            }
        }
    }
}
