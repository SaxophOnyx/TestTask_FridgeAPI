using AutoMapper;
using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using FridgeAPI.Entities.DataTransferObjects.Response;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FridgeAPI.Controllers
{
    [ApiController]
    [Route("/api/fridgemodels")]
    public class FridgeModelsController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IRepositoryManager _repositoryManager;


        public FridgeModelsController(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }


        [HttpGet(Name = nameof(GetAllFridgeModels))]
        public IActionResult GetAllFridgeModels()
        {
            var models = _repositoryManager.FridgeModelRepository.GetModels();

            var modelDTOs = models.Select(m => _mapper.Map<FridgeModelToReturn>(m));
            return Ok(modelDTOs);
        }
    }
}
