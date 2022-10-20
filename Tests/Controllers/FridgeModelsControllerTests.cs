using FridgeAPI.Controllers;
using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using FridgeAPI.Entities.DataTransferObjects.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tests.Mocks;

namespace Tests.Controllers
{
    [TestClass]
    public class FridgeModelsControllerTests
    {
        [TestMethod]
        public void GetAllFridgeModels_Success()
        {
            //arrange
            var controller = CreateController();

            var mapper = MapperMock.CreateMock();
            var expModels = Misc.GenerateFrame().FridgeModels.Select(m => mapper.Object.Map<FridgeModelToReturn>(m));
            var expJson = JsonSerializer.Serialize(expModels);

            //act
            var response = controller.GetAllFridgeModels() as OkObjectResult;
            var models = response.Value as IEnumerable<FridgeModelToReturn>;
            var json = JsonSerializer.Serialize(models);

            //assert
            Assert.AreEqual(((int)HttpStatusCode.OK), response.StatusCode);
            Assert.AreEqual(expJson, json);
        }

        private FridgeModelsController CreateController()
        {
            var frame = Misc.GenerateFrame();

            IFridgeRepository fridgeRepository = new FridgeRepositoryMock(frame.Fridges);
            IProductRepository productRepository = new ProductRepositoryMock(frame.Products);
            IFridgeModelRepository modelRepository = new FridgeModelRepositoryMock(frame.FridgeModels);
            IRepositoryManager manager = new RepositoryManagerMock(fridgeRepository, productRepository, modelRepository);
            var mapperMock = MapperMock.CreateMock();

            return new FridgeModelsController(mapperMock.Object, manager);
        }
    }
}
