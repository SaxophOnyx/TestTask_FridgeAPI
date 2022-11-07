using FridgeAPI.Domain.Contracts.DataTransferObjects.Request;
using FridgeAPI.Domain.Contracts.DataTransferObjects.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeAPI.Domain.Contracts.Interfaces.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductToReturn>> GetProductsAsync();

        Task<IEnumerable<ProductToReturn>> AddProductsAsync(IEnumerable<ProductToCreate> products);

        Task DeleteProductAsync(Guid id);
    }
}
