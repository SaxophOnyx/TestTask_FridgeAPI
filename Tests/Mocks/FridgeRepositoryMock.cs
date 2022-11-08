using FridgeAPI.Domain.Contracts.Exceptions;
using FridgeAPI.Domain.Contracts.Interfaces.Repositories;
using FridgeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    public class FridgeRepositoryMock : IFridgeRepository
    {
        public List<Fridge> Entries { get; }


        public FridgeRepositoryMock(List<Fridge> fridges)
        {
            Entries = fridges;
        }


        public Task<Fridge> AddFridgeAsync(Fridge fridge)
        {
            fridge.Id = Guid.NewGuid();
            Entries.Add(fridge);
            return Task.FromResult(fridge);
        }

        public Task DeleteFridgeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Fridge> GetFridgeAsync(Guid id)
        {
            try
            {
                var fridge = Entries.Single(f => f.Id == id);
                return Task.FromResult(fridge);
            }
            catch(Exception)
            {
                throw new EntityNotFoundException(id, $"Fridge with id {id} not found");
            }
        }

        public Task<IEnumerable<Fridge>> GetFridgesAsync()
        {
            return Task.FromResult(Entries.AsEnumerable());
        }

        public Task<int> SaveAsync()
        {
            return Task.FromResult(0);
        }
    }
}
