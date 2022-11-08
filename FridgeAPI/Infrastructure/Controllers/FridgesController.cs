using FridgeAPI.Domain.Contracts.DataTransferObjects.Request;
using FridgeAPI.Domain.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeAPI.Infrastructure.Controllers
{
    [ApiController]
    [Route("/api/fridges")]
    public class FridgesController : ControllerBase
    {
        private readonly IFridgesService _service;


        public FridgesController(IFridgesService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllFridges()
        {
            var fridges = await _service.GetFridgesAsync();
            return Ok(fridges);
        }

        [HttpPost]
        public async Task<IActionResult> AddFridge([FromBody] FridgeToCreate fridgeDTO)
        {
            var fridge = await _service.AddFridgeAsync(fridgeDTO);
            return Ok(fridge);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFridge([FromRoute(Name = "id")] Guid fridgeId)
        {
            await _service.DeleteFridgrAsync(fridgeId);
            return NoContent();
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetFridgeProducts([FromRoute(Name = "id")] Guid fridgeId)
        {
            var products = await _service.GetProductsInFridgeAsync(fridgeId);
            return Ok(products);
        }

        [HttpPost("{id}/products")]
        public async Task<IActionResult> AddProductsToFridge([FromRoute(Name = "id")] Guid fridgeId, [FromBody] IEnumerable<StoredProductToCreate> productDTOs)
        {
            var addedProducts = await _service.AddProductsToFridgeAsync(fridgeId, productDTOs);
            return Ok(addedProducts);
        }

        [HttpDelete("{id}/products")]
        public async Task<IActionResult> DeleteProductFromFridge([FromRoute(Name = "id")] Guid fridgeId, [FromBody] Guid storedProductId)
        {
            await _service.DeleteProductFromFridgeAsync(fridgeId, storedProductId);
            return NoContent();
        }

        [HttpPut("{id}/products")]
        public async Task<IActionResult> UpdateProductsInFridge([FromRoute(Name = "id")] Guid fridgeId, [FromBody] IEnumerable<StoredProductToUpdate> productDTOs)
        {
            await _service.UpdateProductsInFridgeAsync(fridgeId, productDTOs);
            return NoContent();
        }
    }
}
