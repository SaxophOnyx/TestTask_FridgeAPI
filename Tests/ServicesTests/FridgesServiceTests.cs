using FridgeAPI.Domain.Contracts.DataTransferObjects.Request;
using FridgeAPI.Domain.Contracts.Exceptions;
using FridgeAPI.Domain.Misc;
using FridgeAPI.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.Mocks;
using Xunit;

namespace Tests.ServicesTests
{
    public class FridgesServiceTests
    {
        [Fact]
        public async Task GetFridgesAsync_Ok()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new FridgesService(unitOfWorkMock);
            var expFridges = unitOfWorkMock.FridgeRepositoryMock.Entries.Select(f => f.MapToResponse());

            //act
            var fridges = await service.GetFridgesAsync();

            //assert
            Assert.Equal(expFridges, fridges);
        }

        [Fact]
        public async Task AddFridgeAsync_Ok()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new FridgesService(unitOfWorkMock);
            var modelId = unitOfWorkMock.FridgeModelRepositoryMock.Entries.First().Id;
            var fridgeToCreate = new FridgeToCreate()
            {
                Name = "Test",
                OwnerName = "Owner_Test",
                ModelId = modelId
            };

            var countBefore = unitOfWorkMock.FridgeRepositoryMock.Entries.Count;

            //act
            var returned = await service.AddFridgeAsync(fridgeToCreate);

            //assert
            Assert.Equal(fridgeToCreate.Name, returned.Name);
            Assert.Equal(fridgeToCreate.OwnerName, returned.OwnerName);
            Assert.NotEqual(Guid.Empty, returned.Id);
            Assert.Equal(countBefore + 1, unitOfWorkMock.FridgeRepositoryMock.Entries.Count);
        }

        [Fact]
        public async Task AddProductsToFridgeAsync_Ok()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new FridgesService(unitOfWorkMock);

            var fridge = unitOfWorkMock.FridgeRepositoryMock.Entries.First();
            var countBefore = fridge.Products.Count;
            var productId = unitOfWorkMock.ProductRepositoryMock.Entries.First().Id;
            var productToAdd = new StoredProductToCreate()
            {
                ProductId = productId,
                Quantity = 120
            };

            //act
            var productToReturn = await service.AddProductsToFridgeAsync(fridge.Id, new List<StoredProductToCreate>() { productToAdd });

            //assert
            var returned = Assert.Single(productToReturn);
            Assert.Equal(productToAdd.Quantity, returned.Quantity);
            Assert.Equal(productToAdd.ProductId, returned.ProductId);
            Assert.Equal(countBefore + 1, unitOfWorkMock.FridgeRepositoryMock.Entries.First().Products.Count);
        }

        [Fact]
        public async Task AddProductsToFridgeAsync_ProductNotFound()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new FridgesService(unitOfWorkMock);

            var fridgeId = unitOfWorkMock.FridgeRepositoryMock.Entries.First().Id;
            var productToAdd = new StoredProductToCreate()
            {
                ProductId = Guid.NewGuid(),
                Quantity = 120
            };

            //act + assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await service.AddProductsToFridgeAsync(fridgeId, new List<StoredProductToCreate>() { productToAdd }));
        }

        [Fact]
        public async Task DeleteProductFromFridgeAsync_Ok()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new FridgesService(unitOfWorkMock);

            var fridge = unitOfWorkMock.FridgeRepositoryMock.Entries.First();
            var productId = fridge.Products.First().Id;
            var countBefore = fridge.Products.Count;

            //act
            await service.DeleteProductFromFridgeAsync(fridge.Id, productId);

            //assert
            Assert.Equal(countBefore - 1, unitOfWorkMock.FridgeRepositoryMock.Entries.First(f => f.Id == fridge.Id).Products.Count);
            Assert.Null(unitOfWorkMock.FridgeRepositoryMock.Entries.First(f => f.Id == fridge.Id).Products.FirstOrDefault(p => p.Id == productId));
        }

        [Fact]
        public async Task UpdateProductsInFridgeAsync_Ok()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new FridgesService(unitOfWorkMock);

            var fridge = unitOfWorkMock.FridgeRepositoryMock.Entries.First();
            var productId = fridge.Products.First().Id;
            var countBefore = fridge.Products.Count;
            var productToUpdate = new StoredProductToUpdate()
            {
                Id = productId,
                Quantity = 25001
            };

            //act
            await service.UpdateProductsInFridgeAsync(fridge.Id, new List<StoredProductToUpdate>() { productToUpdate });
            var updated = unitOfWorkMock.FridgeRepositoryMock.Entries.First(f => f.Id == fridge.Id).Products.First(u => u.Id == productId);

            //assert
            Assert.Equal(countBefore, unitOfWorkMock.FridgeRepositoryMock.Entries.Count);
            Assert.NotNull(updated);
            Assert.Equal(productToUpdate.Quantity, updated.Quantity);
        }

        [Fact]
        public async Task UpdateProductsInFridgeAsync_ProductNotFound()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new FridgesService(unitOfWorkMock);

            var fridge = unitOfWorkMock.FridgeRepositoryMock.Entries.First();
            var productId = Guid.NewGuid();
            var productToUpdate = new StoredProductToUpdate()
            {
                Id = productId,
                Quantity = 25001
            };

            //act + assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async ()
                => await service.UpdateProductsInFridgeAsync(fridge.Id, new List<StoredProductToUpdate>() { productToUpdate }));
        }
    }
}
