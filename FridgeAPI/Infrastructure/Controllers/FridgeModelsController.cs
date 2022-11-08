using FridgeAPI.Domain.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FridgeAPI.Infrastructure.Controllers
{
    [ApiController]
    [Route("/api/fridgemodels")]
    public class FridgeModelsController: ControllerBase
    {
        private readonly IFridgeModelsService _service;


        public FridgeModelsController(IFridgeModelsService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetFridgeModels()
        {
            var models = await _service.GetFridgeModelsAsync();
            return Ok(models);
        }
    }
}
