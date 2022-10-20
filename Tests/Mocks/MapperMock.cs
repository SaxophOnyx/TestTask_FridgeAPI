using AutoMapper;
using FridgeAPI.Entities.DataTransferObjects.Request;
using FridgeAPI.Entities.DataTransferObjects.Response;
using FridgeAPI.Entities.Models;
using Moq;
using System;
using System.Collections.Generic;

namespace Tests.Mocks
{
    public static class MapperMock
    {
        public static Mock<IMapper> CreateMock()
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<FridgeToReturn>(It.IsAny<Fridge>())).Returns<Fridge>(MapFridgeToReturn);
            mapperMock.Setup(m => m.Map<Fridge>(It.IsAny<FridgeToCreate>())).Returns<FridgeToCreate>(MapFridge);
            mapperMock.Setup(m => m.Map<StoredProductToReturn>(It.IsAny<StoredProduct>())).Returns<StoredProduct>(MapStoredProductToReturn);
            mapperMock.Setup(m => m.Map<StoredProduct>(It.IsAny<StoredProductToCreate>())).Returns<StoredProductToCreate>(MapStoredProduct);
            mapperMock.Setup(m => m.Map<FridgeModelToReturn>(It.IsAny<FridgeModel>())).Returns<FridgeModel>(MapFridgeModelToReturn);
            mapperMock.Setup(m => m.Map<ProductToReturn>(It.IsAny<Product>())).Returns<Product>(MapProductToReturn);
            mapperMock.Setup(m => m.Map<Product>(It.IsAny<ProductToCreate>())).Returns<ProductToCreate>(MapProduct);

            return mapperMock;
        }

        private static FridgeToReturn MapFridgeToReturn(Fridge f)
        {
            return new FridgeToReturn()
            {
                Id = f.Id,
                Name = f.Name,
                OwnerName = f.OwnerName,
                Model = new FridgeModelToReturn()
                {
                    Id = f.Model.Id,
                    Name = f.Model.Name,
                    Year = f.Model.Year
                }
            };
        }

        private static Fridge MapFridge(FridgeToCreate f)
        {
            return new Fridge()
            {
                Model = new FridgeModel()
                {
                    Id = (System.Guid)f.ModelId,
                },
                Name = f.Name,
                OwnerName = f.OwnerName,
                Products = new List<StoredProduct>()
            };
        }

        private static StoredProductToReturn MapStoredProductToReturn(StoredProduct p)
        {
            return new StoredProductToReturn()
            {
                ProductId = p.Product.Id,
                ProductName = p.Product.Name,
                Quantity = p.Quantity
            };
        }

        private static StoredProduct MapStoredProduct(StoredProductToCreate p)
        {
            return new StoredProduct()
            {
                Quantity = (int)p.Quantity,
                Id = (Guid)p.ProductId
            };
        }

        private static FridgeModelToReturn MapFridgeModelToReturn(FridgeModel model)
        {
            return new FridgeModelToReturn()
            {
                Id = model.Id,
                Year = model.Year,
                Name = model.Name
            };
        }

        private static ProductToReturn MapProductToReturn(Product p)
        {
            return new ProductToReturn()
            {
                Id = p.Id,
                Name = p.Name,
                DefaultQuantity = p.DefaultQuantity
            };
        }

        private static Product MapProduct(ProductToCreate p)
        {
            return new Product()
            {
                Name = p.Name,
                DefaultQuantity = p.DefaultQuantity
            };
        }
    }
}
