using FridgeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeAPI.Domain.Contracts.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductAsync(Guid id);

        Task<List<Product>> AddProductsAsync(List<Product> products);

        Task DeleteProductAsync(Guid id);

        Task<int> SaveAsync();
    }
}
