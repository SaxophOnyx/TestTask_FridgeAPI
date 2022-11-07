using FridgeAPI.Domain.Contracts.DataTransferObjects.Request;
using FridgeAPI.Domain.Contracts.DataTransferObjects.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeAPI.Domain.Contracts.Interfaces.Services
{
    public interface IFridgesService
    {
        Task<IEnumerable<FridgeToReturn>> GetFridgesAsync();

        Task<FridgeToReturn> AddFridgeAsync(FridgeToCreate fridge);

        Task DeleteFridgrAsync(Guid fridgeId);

        Task<IEnumerable<StoredProductToReturn>> GetProductsInFridgeAsync(Guid fridgeId);

        Task<IEnumerable<StoredProductToReturn>> AddProductsToFridgeAsync(Guid fridgeId, IEnumerable<StoredProductToCreate> products);

        Task UpdateProductsInFridgeAsync(Guid fridgeId, IEnumerable<StoredProductToUpdate> products);

        Task DeleteProductFromFridgeAsync(Guid fridgeId, Guid storedProductId);
    }
}
