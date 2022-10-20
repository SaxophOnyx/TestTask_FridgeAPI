using AutoMapper;
using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using FridgeAPI.Entities.DataTransferObjects.Request;
using FridgeAPI.Entities.DataTransferObjects.Response;
using FridgeAPI.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FridgeAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IRepositoryManager _repositoryManager;


        public ProductsController(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _repositoryManager.ProductRepository.GetProducts();
            var productDTOs = products.Select(p => _mapper.Map<ProductToReturn>(p));

            return Ok(productDTOs);
        }

        [HttpPost]
        public IActionResult AddProducts([FromBody] IEnumerable<ProductToCreate> productDTOs)
        {
            if ((productDTOs == null) || (!ModelState.IsValid))
                return UnprocessableEntity(ModelState);

            var productsToReturn = new List<ProductToReturn>();

            foreach (var dto in productDTOs)
            {
                var product = _mapper.Map<Product>(dto);
                _repositoryManager.ProductRepository.AddProduct(product);

                var productToReturn = _mapper.Map<ProductToReturn>(product);
                productsToReturn.Add(productToReturn);
            }

            _repositoryManager.ProductRepository.Save();

            return Created("/api/products", productsToReturn);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteProduct([FromRoute(Name = "id")] Guid productId)
        {
            _repositoryManager.ProductRepository.DeleteProduct(productId);
            _repositoryManager.ProductRepository.Save();

            return NoContent();
        }
    }
}
