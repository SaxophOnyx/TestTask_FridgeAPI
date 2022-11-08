using FridgeAPI.Domain.Contracts.DataTransferObjects.Response;
using FridgeAPI.Domain.Contracts.Interfaces.Repositories;
using FridgeAPI.Domain.Contracts.Interfaces.Services;
using FridgeAPI.Domain.Misc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeAPI.Domain.Services
{
    public class FridgeModelsService : ServiceBase, IFridgeModelsService
    {
        public FridgeModelsService(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }


        public async Task<IEnumerable<FridgeModelToReturn>> GetFridgeModelsAsync()
        {
            var models = await _unitOfWork.FridgeModelRepository.GetFridgeModelsAsync();
            return models.Select(model => model.MapToResponse());
        }
    }
}
