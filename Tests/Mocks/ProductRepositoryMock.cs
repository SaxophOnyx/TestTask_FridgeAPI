using FridgeAPI.Domain.Contracts.Exceptions;
using FridgeAPI.Domain.Contracts.Interfaces.Repositories;
using FridgeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    public class ProductRepositoryMock : IProductRepository
    {
        public List<Product> Entries { get; }


        public ProductRepositoryMock(List<Product> products)
        {
            Entries = products;
        }

        public Task<List<Product>> AddProductsAsync(List<Product> products)
        {
            products.ForEach(p => p.Id = Guid.NewGuid());
            Entries.AddRange(products);
            return Task.FromResult(products);
        }

        public Task DeleteProductAsync(Guid id)
        {
            var entry = Entries.FirstOrDefault(p => p.Id == id);

            if (entry != null)
                Entries.Remove(entry);
            return Task.CompletedTask;
        }

        public Task<Product> GetProductAsync(Guid id)
        {
            try
            {
                var entry = Entries.Single(p => p.Id == id);
                return Task.FromResult(entry);
            }
            catch
            {
                throw new EntityNotFoundException(id, $"Product with id {id} not found");
            }
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            return Task.FromResult(Entries.AsEnumerable());
        }

        public Task<int> SaveAsync()
        {
            return Task.FromResult(0);
        }
    }
}
