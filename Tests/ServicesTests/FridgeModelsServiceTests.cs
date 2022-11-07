using Xunit;
using System.Threading.Tasks;
using Tests.Mocks;
using FridgeAPI.Domain.Services;
using System.Linq;
using FridgeAPI.Domain.Misc;

namespace Tests.ServicesTests
{
    public class FridgeModelsServiceTests
    {
        [Fact]
        public async Task GetFridgeModelsAsync_Ok()
        {
            //arrange
            var unitOfWorkMock = new UnitOfWorkMock();
            var service = new FridgeModelsService(unitOfWorkMock);
            var expModels = unitOfWorkMock.FridgeModelRepositoryMock.Entries.Select(m => m.MapToResponse());

            //act
            var models = await service.GetFridgeModelsAsync();

            //assert
            Assert.Equal(expModels, models);
        }
    }
}
