using FridgeAPI.Domain.Contracts.DataTransferObjects.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeAPI.Domain.Contracts.Interfaces.Services
{
    public interface IFridgeModelsService
    {
        Task<IEnumerable<FridgeModelToReturn>> GetFridgeModelsAsync();
    }
}
