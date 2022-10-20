using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Mocks;
using System.Collections.Generic;
using FridgeAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using FridgeAPI.Entities.DataTransferObjects.Response;
using System.Linq;
using System.Text.Json;
using System;
using System.Net;
using FridgeAPI.Entities.DataTransferObjects.Request;

namespace Tests.Controllers
{
    [TestClass]
    public class FridgeControllerTests
    {
        [TestMethod]
        public void GetAllFridges_ReturnsAllFridges()
        {
            //arrange
            var controller = CreateController();

            var mapperMock = MapperMock.CreateMock();
            var expFridges = Misc.GenerateFrame().Fridges.Select(f => mapperMock.Object.Map<FridgeToReturn>(f)).ToList();
            var expFridgesJson = JsonSerializer.Serialize(expFridges);

            //act
            var result = controller.GetAllFridges() as OkObjectResult;
            var fridges = (result.Value as IEnumerable<FridgeToReturn>).ToList();
            var fridgesJson = JsonSerializer.Serialize(fridges);

            //assert
            Assert.AreEqual(((int)HttpStatusCode.OK), result.StatusCode);
            Assert.AreEqual(expFridgesJson, fridgesJson);
        }

        [TestMethod]
        public void AddFridge_ModelNotFound()
        {
            //arrange
            var controller = CreateController();
            var fridgeToCreate = new FridgeToCreate()
            {
                ModelId = Misc.GetGuid("111"),
                Name = "Name",
                OwnerName = "Owner"
            };

            //act
            var response = controller.AddFridge(fridgeToCreate) as NotFoundResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.NotFound), response.StatusCode);
        }

        [TestMethod]
        public void AddFridge_Created()
        {
            //arrange
            var controller = CreateController();
            var fridgeToCreate = new FridgeToCreate()
            {
                ModelId = Misc.GetGuid("1"),
                Name = "Name",
                OwnerName = "Owner"
            }; 

            //act
            var response = controller.AddFridge(fridgeToCreate) as CreatedResult;
            var fridge = response.Value as FridgeToReturn;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.Created), response.StatusCode);
            Assert.IsInstanceOfType(fridge, typeof(FridgeToReturn));
            Assert.AreEqual(fridgeToCreate.Name, fridge.Name);
            Assert.AreEqual(fridgeToCreate.OwnerName, fridge.OwnerName);
            Assert.AreEqual(fridgeToCreate.ModelId, fridge.Model.Id);
        }

        [TestMethod]
        public void DeleteFridge_Deleted()
        {
            //arrange
            var controller = CreateController();
            var fridgeId = Misc.GetGuid("12");

            //act
            var response = controller.DeleteFridge(fridgeId) as NoContentResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.NoContent), response.StatusCode);
        }

        [TestMethod]
        public void GetFridgeProducts_NotFound()
        {
            //arrange
            var controller = CreateController();
            var fridgeId = Misc.GetGuid("9999");

            //act
            var response = controller.GetFridgeProducts(fridgeId) as NotFoundResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.NotFound), response.StatusCode);
        }

        [TestMethod]
        public void GetFridgeProducts_ReturnsFridgeProducts()
        {
            //arrange
            var frame = Misc.GenerateFrame();
            IFridgeRepository fridgeRepository = new FridgeRepositoryMock(frame.Fridges);
            IProductRepository productRepository = new ProductRepositoryMock(frame.Products);
            IFridgeModelRepository modelRepository = new FridgeModelRepositoryMock(frame.FridgeModels);
            IRepositoryManager manager = new RepositoryManagerMock(fridgeRepository, productRepository, modelRepository);
            var mapperMock = MapperMock.CreateMock();
            var controller = new FridgesController(mapperMock.Object, manager);

            var fridgeId = Misc.GetGuid("12");
            var expProducts = fridgeRepository.GetFridge(fridgeId).Products;

            //act
            var response = controller.GetFridgeProducts(fridgeId) as OkObjectResult;
            var products = response.Value as IEnumerable<StoredProductToReturn>;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.OK), response.StatusCode);
            Assert.IsInstanceOfType(products, typeof(IEnumerable<StoredProductToReturn>));

            var listOfProducts = products.ToList();
            Assert.AreEqual(expProducts.Count, listOfProducts.Count);
            for (int i = 0; i < expProducts.Count; ++i)
            {
                Assert.AreEqual(expProducts[i].Product.Id, listOfProducts[i].ProductId);
                Assert.AreEqual(expProducts[i].Quantity, listOfProducts[i].Quantity);
                Assert.AreEqual(expProducts[i].Product.Name, listOfProducts[i].ProductName);
            }
        }

        [TestMethod]
        public void AddProductsToFridge_UnprocessableEntity()
        {
            //arrange
            var controller = CreateController();
            controller.ModelState.AddModelError("key", "message");

            //act
            var response = controller.AddProductsToFridge(Guid.NewGuid(), new List<StoredProductToCreate>()) as UnprocessableEntityObjectResult;
            controller.ModelState.Clear();
            var nullResponse = controller.AddProductsToFridge(Guid.NewGuid(), null) as UnprocessableEntityObjectResult;
            
            //assert
            Assert.AreEqual(((int)HttpStatusCode.UnprocessableEntity), response.StatusCode);
            Assert.AreEqual(((int)HttpStatusCode.UnprocessableEntity), nullResponse.StatusCode);
        }

        [TestMethod]
        public void AddProductsToFridge_FridgeNotFound()
        {
            //arrange
            var controller = CreateController();
            var fridgeId = Misc.GetGuid("9999");
            var products = new List<StoredProductToCreate>()
            {
                new StoredProductToCreate()
                {
                    ProductId = Misc.GetGuid("4"),
                    Quantity = 100
                }
            };

            //act
            var response = controller.AddProductsToFridge(fridgeId, products) as NotFoundResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.NotFound), response.StatusCode);
        }

        [TestMethod]
        public void AddProductFromFridge_Success()
        {
            //arrange
            var controller = CreateController();
            var fridgeId = Misc.GetGuid("12");
            var products = new List<StoredProductToCreate>()
            {
                new StoredProductToCreate()
                {
                    ProductId = Misc.GetGuid("5"),
                    Quantity = 200
                }
            };

            //act
            var response = controller.AddProductsToFridge(fridgeId, products) as CreatedResult;
            var returned = response.Value as IEnumerable<StoredProductToReturn>;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.Created), response.StatusCode);
            Assert.AreEqual(products.First().ProductId, returned.First().ProductId);
            Assert.AreEqual(products.First().Quantity, returned.First().Quantity);
        }

        [TestMethod]
        public void AddProductFromFridge_ProductNotFound()
        {
            //arrange
            var controller = CreateController();
            var fridgeId = Misc.GetGuid("12");
            var products = new List<StoredProductToCreate>()
            {
                new StoredProductToCreate()
                {
                    ProductId = Misc.GetGuid("9999"),
                    Quantity = 200
                }
            };

            //act
            var response = controller.AddProductsToFridge(fridgeId, products) as NotFoundResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.NotFound), response.StatusCode);
        }

        [TestMethod]
        public void DeleteProductFromFridge_FridgeNotFound()
        {
            //arrange
            var controller = CreateController();
            var fridgeId = Misc.GetGuid("9999");

            //act
            var response = controller.DeleteProductFromFridge(fridgeId, Misc.GetGuid("9999")) as NotFoundResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.NotFound), response.StatusCode);
        }

        [TestMethod]
        public void DeleteProductFromFridge_Success()
        {
            //arrange
            var controller = CreateController();
            var fridgeId = Misc.GetGuid("12");
            var productId = Misc.GetGuid("7");

            //act
            var response = controller.DeleteProductFromFridge(fridgeId, productId) as NoContentResult;

            //asssert
            Assert.AreEqual(((int)HttpStatusCode.NoContent), response.StatusCode);
        }

        [TestMethod]
        public void UpdateProductInFridge_UnprocessableEntity()
        {
            //arrange
            var controller = CreateController();
            controller.ModelState.AddModelError("key", "message");
            var fridgeId = Misc.GetGuid("12");
            var products = new List<StoredProductToUpdate>();

            //act
            var response = controller.UpdateProductsInFridge(fridgeId, products) as UnprocessableEntityObjectResult;
            controller.ModelState.Clear();
            var nullResponse = controller.UpdateProductsInFridge(fridgeId, null) as UnprocessableEntityObjectResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.UnprocessableEntity), response.StatusCode);
            Assert.AreEqual(((int)HttpStatusCode.UnprocessableEntity), nullResponse.StatusCode);
        }

        public void UpdateProductInFridge_FridgeNotFound()
        {
            //arrange
            var controller = CreateController();
            var fridgeId = Misc.GetGuid("9999");
            var products = new List<StoredProductToUpdate>();

            //act
            var response = controller.UpdateProductsInFridge(fridgeId, products) as NotFoundResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.UnprocessableEntity), response.StatusCode);
        }

        [TestMethod]
        public void UpdateProductInFridge_ProductNotFound()
        {
            //arrange
            var controller = CreateController();
            var fridgeId = Misc.GetGuid("12");
            var products = new List<StoredProductToUpdate>()
            {
                new StoredProductToUpdate()
                {
                    Id = Misc.GetGuid("9999"),
                    Quantity = 100
                }
            };

            //act
            var response = controller.UpdateProductsInFridge(fridgeId, products) as NotFoundResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.NotFound), response.StatusCode);
        }

        [TestMethod]
        public void UpdateProductInFridge_Success()
        {
            //arrange
            var controller = CreateController();
            var fridgeId = Misc.GetGuid("12");
            var products = new List<StoredProductToUpdate>()
            {
                new StoredProductToUpdate()
                {
                    Id = Misc.GetGuid("7"),
                    Quantity = 100
                }
            };

            //act
            var response = controller.UpdateProductsInFridge(fridgeId, products) as NoContentResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.NoContent), response.StatusCode);
        }

        private FridgesController CreateController()
        {
            var frame = Misc.GenerateFrame();

            IFridgeRepository fridgeRepository = new FridgeRepositoryMock(frame.Fridges);
            IProductRepository productRepository = new ProductRepositoryMock(frame.Products);
            IFridgeModelRepository modelRepository = new FridgeModelRepositoryMock(frame.FridgeModels);
            IRepositoryManager manager = new RepositoryManagerMock(fridgeRepository, productRepository, modelRepository);
            var mapperMock = MapperMock.CreateMock();

            return new FridgesController(mapperMock.Object, manager);
        }
    }
}
