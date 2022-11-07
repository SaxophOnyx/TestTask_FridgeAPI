using FridgeAPI.Domain.Contracts.DataTransferObjects.Request;
using FridgeAPI.Domain.Contracts.DataTransferObjects.Response;
using FridgeAPI.Domain.Contracts.Exceptions;
using FridgeAPI.Domain.Contracts.Interfaces.Repositories;
using FridgeAPI.Domain.Contracts.Interfaces.Services;
using FridgeAPI.Domain.Entities;
using FridgeAPI.Domain.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeAPI.Domain.Services
{
    public class FridgesService : ServiceBase, IFridgesService
    {
        public FridgesService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }


        public async Task<IEnumerable<FridgeToReturn>> GetFridgesAsync()
        {
            var fridges = await _unitOfWork.FridgeRepository.GetFridgesAsync();
            return fridges.Select(f => f.MapToResponse());
        }

        public async Task<FridgeToReturn> AddFridgeAsync(FridgeToCreate fridge)
        {
            var model = await _unitOfWork.FridgeModelRepository.GetFridgeModelAsync((Guid)fridge.ModelId);
            var fridgeToAdd = new Fridge()
            {
                Name = fridge.Name,
                OwnerName = fridge.OwnerName,
                Products = new List<StoredProduct>(),
                Model = model
            };

            var createdFridge = await _unitOfWork.FridgeRepository.AddFridgeAsync(fridgeToAdd);
            await _unitOfWork.FridgeRepository.SaveAsync();
            return createdFridge.MapToResponse();
        }

        public async Task DeleteFridgrAsync(Guid fridgeId)
        {
            await _unitOfWork.FridgeRepository.DeleteFridgeAsync(fridgeId);
            await _unitOfWork.FridgeRepository.SaveAsync();
        }

        public async Task<IEnumerable<StoredProductToReturn>> GetProductsInFridgeAsync(Guid fridgeId)
        {
            var fridge = await _unitOfWork.FridgeRepository.GetFridgeAsync(fridgeId);
            return fridge.Products.Select(p => p.MapToResponse());
        }

        public async Task<IEnumerable<StoredProductToReturn>> AddProductsToFridgeAsync(Guid fridgeId, IEnumerable<StoredProductToCreate> products)
        {
            var fridge = await _unitOfWork.FridgeRepository.GetFridgeAsync(fridgeId);
            var avaliableProducts = await _unitOfWork.ProductRepository.GetProductsAsync();

            var productsToAdd = new List<StoredProduct>();
            foreach (var product in products)
            {
                Product avaliable;

                try
                {
                    avaliable = avaliableProducts.Single(pr => pr.Id == product.ProductId);
                }
                catch (Exception)
                {
                    throw new EntityNotFoundException((Guid)product.ProductId, $"Product with id {product.ProductId} not found");
                }

                var productToAdd = new StoredProduct()
                {
                    Id = Guid.Empty,
                    Product = avaliable,
                    Quantity = (int)product.Quantity
                };

                productsToAdd.Add(productToAdd);
            }

            fridge.Products.AddRange(productsToAdd);
            await _unitOfWork.FridgeRepository.SaveAsync();

            return productsToAdd.Select(p => p.MapToResponse());
        }

        public async Task UpdateProductsInFridgeAsync(Guid fridgeId, IEnumerable<StoredProductToUpdate> products)
        {
            var fridge = await _unitOfWork.FridgeRepository.GetFridgeAsync(fridgeId);

            foreach (var product in products)
            {
                if (fridge.Products.FirstOrDefault(p => p.Id == product.Id) == null)
                    throw new EntityNotFoundException((Guid)product.Id, $"Stored product with id {product.Id} not found");
            }

            foreach (var product in products)
            {
                var fridgeProduct = fridge.Products.Single(p => p.Id == product.Id);
                fridgeProduct.Quantity = (int)product.Quantity;
            }

            await _unitOfWork.FridgeRepository.SaveAsync();
        }

        public async Task DeleteProductFromFridgeAsync(Guid fridgeId, Guid storedProductId)
        {
            var fridge = await _unitOfWork.FridgeRepository.GetFridgeAsync(fridgeId);
            fridge.Products.RemoveAll(p => p.Id == storedProductId);
            await _unitOfWork.FridgeRepository.SaveAsync();
        }
    }
}
