using FridgeAPI.Domain.Contracts.DataTransferObjects.Request;
using FridgeAPI.Domain.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FridgeAPI.Infrastructure.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _service;


        public ProductsController(IProductsService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducts([FromBody] IEnumerable<ProductToCreate> productDTOs)
        {
            var productsToReturn = await _service.AddProductsAsync(productDTOs);
            return Created("/api/products", productsToReturn);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute(Name = "id")] Guid productId)
        {
            await _service.DeleteProductAsync(productId);
            return NoContent();
        }
    }
}
