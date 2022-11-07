using FridgeAPI.Domain.Entities;
using FridgeAPI.Domain.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class ResponseMappingExtensionsTests
    {
        [Fact]
        public void Fridge_MapToResponse_Test()
        {
            //arrange
            var fridge = GenerateRandomFridge();

            //act
            var response = fridge.MapToResponse();

            //assert
            Assert.Equal(fridge.Name, response.Name);
            Assert.Equal(fridge.Id, response.Id);
            Assert.Equal(fridge.OwnerName, response.OwnerName);
            Assert.Equal(fridge.Model.MapToResponse(), response.Model);
        }

        [Fact]
        public void FridgeModel_MapToResponse_Test()
        {
            //arrange
            var model = GenerateRandomModel();

            //act
            var response = model.MapToResponse();

            //assert
            Assert.Equal(model.Name, response.Name);
            Assert.Equal(model.Id, response.Id);
            Assert.Equal(model.Year, response.Year);
        }

        [Fact]
        public void StoredProduct_MapToResponse_Test()
        {
            //arrange
            var stored = GenerateRandomStoredProduct();

            //act
            var response = stored.MapToResponse();

            //assert
            Assert.Equal(stored.Id, response.Id);
            Assert.Equal(stored.Quantity, response.Quantity);
            Assert.Equal(stored.Product.Name, response.ProductName);
            Assert.Equal(stored.Product.Id, response.ProductId);
        }

        [Fact]
        public void Product_MapToResponse_Test()
        {
            //arrange
            var product = GenerateRandomProduct();

            //act
            var response = product.MapToResponse();

            //assert
            Assert.Equal(product.Name, response.Name);
            Assert.Equal(product.Id, response.Id);
            Assert.Equal(product.DefaultQuantity, response.DefaultQuantity);
        }

        private string GenerateRandomString()
        {
            Random rand = new Random();
            
            int length = rand.Next(100);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        private Product GenerateRandomProduct()
        {
            Random rand = new Random();

            return new Product()
            {
                Id = Guid.NewGuid(),
                DefaultQuantity = rand.Next(0, int.MaxValue),
                Name = GenerateRandomString()
            };
        }

        private StoredProduct GenerateRandomStoredProduct()
        {
            Random rand = new Random();

            return new StoredProduct()
            {
                Id = Guid.NewGuid(),
                Quantity = rand.Next(0, int.MaxValue),
                Product = GenerateRandomProduct()
            };
        }

        private FridgeModel GenerateRandomModel()
        {
            Random rand = new Random();

            return new FridgeModel()
            {
                Id = Guid.NewGuid(),
                Name = GenerateRandomString(),
                Year = rand.Next(0, int.MaxValue)
            };
        }

        private Fridge GenerateRandomFridge()
        {
            Random rand = new Random();
            int length = rand.Next(1, 11);

            List<StoredProduct> generatedProduct = new List<StoredProduct>();
            for (int i = 0; i < length; ++i)
                generatedProduct.Add(GenerateRandomStoredProduct());

            return new Fridge()
            {
                Id = Guid.NewGuid(),
                OwnerName = GenerateRandomString(),
                Name = GenerateRandomString(),
                Model = GenerateRandomModel(),
                Products = generatedProduct
            };
        }
    }
}
