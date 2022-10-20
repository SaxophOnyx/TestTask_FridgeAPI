using FridgeAPI.Controllers;
using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using FridgeAPI.Entities.DataTransferObjects.Request;
using FridgeAPI.Entities.DataTransferObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using Tests.Mocks;

namespace Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        [TestMethod]
        public void GetAllProducts_ReturnsAllProducts()
        {
            //arrange
            var controller = CreateController();
            var mapper = MapperMock.CreateMock();
            var expProducts = Misc.GenerateFrame().Products.Select(p => mapper.Object.Map<ProductToReturn>(p));
            var expJson = JsonSerializer.Serialize(expProducts);

            //act
            var response = controller.GetAllProducts() as OkObjectResult;
            var products = response.Value as IEnumerable<ProductToReturn>;
            var json = JsonSerializer.Serialize(products);

            //assert
            Assert.AreEqual(((int)HttpStatusCode.OK), response.StatusCode);
            Assert.AreEqual(expJson, json);
        }

        [TestMethod]
        public void AddProducts_UnprocessableEntity()
        {
            //arrange
            var controller = CreateController();
            controller.ModelState.AddModelError("key", "message");
            var products = new List<ProductToCreate>();

            //act
            var response = controller.AddProducts(products) as UnprocessableEntityObjectResult;
            controller.ModelState.Clear();
            var nullResponse = controller.AddProducts(null) as UnprocessableEntityObjectResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.UnprocessableEntity), response.StatusCode);
            Assert.AreEqual(((int)HttpStatusCode.UnprocessableEntity), nullResponse.StatusCode);
        }

        [TestMethod]
        public void AddProducts_Success()
        {
            //arrange
            var controller = CreateController();
            var products = new List<ProductToCreate>()
            {
                new ProductToCreate()
                {
                    DefaultQuantity = 1111,
                    Name = "Name_1"
                },

                new ProductToCreate()
                {
                    DefaultQuantity = 2222,
                    Name = "Name_2"
                }
            };

            //act
            var response = controller.AddProducts(products) as CreatedResult;
            var created = response.Value as IEnumerable<ProductToReturn>;
            var createdList = created.ToList();

            //assert
            Assert.IsInstanceOfType(created, typeof(IEnumerable<ProductToReturn>));
            for (int i = 0; i < createdList.Count; ++i)
            {
                Assert.AreEqual(products[i].Name, createdList[i].Name);
                Assert.AreEqual(products[i].DefaultQuantity, createdList[i].DefaultQuantity);
            }
        }

        [TestMethod]
        public void DeleteProduct_Success()
        {
            //arrange
            var controller = CreateController();
            var productId = Misc.GetGuid("4");

            //act
            var response = controller.DeleteProduct(productId) as NoContentResult;

            //assert
            Assert.AreEqual(((int)HttpStatusCode.NoContent), response.StatusCode);
        }

        private ProductsController CreateController()
        {
            var frame = Misc.GenerateFrame();

            IFridgeRepository fridgeRepository = new FridgeRepositoryMock(frame.Fridges);
            IProductRepository productRepository = new ProductRepositoryMock(frame.Products);
            IFridgeModelRepository modelRepository = new FridgeModelRepositoryMock(frame.FridgeModels);
            IRepositoryManager manager = new RepositoryManagerMock(fridgeRepository, productRepository, modelRepository);
            var mapperMock = MapperMock.CreateMock();

            return new ProductsController(mapperMock.Object, manager);
        }
    }
}
