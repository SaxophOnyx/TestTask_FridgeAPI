using FridgeAPI.Domain.Contracts.DataTransferObjects.Request;
using FridgeAPI.Domain.Services;
using FridgeAPI.Domain.Misc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.Mocks;
using Xunit;
using System;

namespace Tests.ServicesTests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsAsync_Ok()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new ProductsService(unitOfWorkMock);
            var expProducts = unitOfWorkMock.ProductRepositoryMock.Entries.Select(p => p.MapToResponse());

            //act
            var products = await service.GetProductsAsync();

            //assert
            Assert.Equal(expProducts, products);
        }

        [Fact]
        public async Task AddProductsAsync_Ok()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new ProductsService(unitOfWorkMock);
            var countBefore = unitOfWorkMock.ProductRepositoryMock.Entries.Count;
            var productToAdd = new ProductToCreate()
            {
                Name = "Test",
                DefaultQuantity = 100,
            };

            //act
            var returnedProducts = await service.AddProductsAsync(new List<ProductToCreate>() { productToAdd } );

            //assert
            Assert.Equal(countBefore + 1, unitOfWorkMock.ProductRepositoryMock.Entries.Count);
            var returned = Assert.Single(returnedProducts);
            Assert.Equal(productToAdd.Name, returned.Name);
            Assert.Equal(productToAdd.DefaultQuantity, returned.DefaultQuantity);
            Assert.NotEqual(Guid.Empty, returned.Id);
        }

        [Fact]
        public async Task DeleteProductAsync_Ok()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new ProductsService(unitOfWorkMock);
            var countBefore = unitOfWorkMock.ProductRepositoryMock.Entries.Count;
            var productId = unitOfWorkMock.ProductRepositoryMock.Entries.First().Id;

            //act
            await service.DeleteProductAsync(productId);

            //assert
            Assert.Equal(countBefore - 1, unitOfWorkMock.ProductRepositoryMock.Entries.Count);
        }
    }
}
