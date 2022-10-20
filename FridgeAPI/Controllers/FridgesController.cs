using AutoMapper;
using FridgeAPI.Entities.DataTransferObjects.Request;
using FridgeAPI.Entities.DataTransferObjects.Response;
using FridgeAPI.Entities.Models;
using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace FridgeAPI.Controllers
{
    [ApiController]
    [Route("api/fridges")]
    public class FridgesController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IRepositoryManager _repositoryManager;


        public FridgesController(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }


        [HttpGet(Name = nameof(GetAllFridges))]
        public IActionResult GetAllFridges()
        {
            var fridges = _repositoryManager.FridgeRepository.GetFridges();
            var fridgeDTOs = fridges.Select(f => _mapper.Map<FridgeToReturn>(f));

            return Ok(fridgeDTOs);
        }

        [HttpPost(Name = nameof(AddFridge))]
        public IActionResult AddFridge([FromBody] FridgeToCreate fridgeDTO)
        {
            var model = _repositoryManager.FridgeModelRepository.GetModel((Guid)fridgeDTO.ModelId);

            if (model == null)
                return NotFound();

            var fridge = _mapper.Map<Fridge>(fridgeDTO);
            fridge.Model = model;

            _repositoryManager.FridgeRepository.AddFridge(fridge);
            _repositoryManager.FridgeRepository.Save();

            var fridgeToReturn = _mapper.Map<FridgeToReturn>(fridge);
            return Created($"api/fridges", fridgeToReturn);
        }

        [HttpDelete("{id}", Name = nameof(DeleteFridge))]
        public IActionResult DeleteFridge([FromRoute(Name = "id")] Guid fridgeId)
        {
            _repositoryManager.FridgeRepository.DeleteFridge(fridgeId);
            _repositoryManager.FridgeRepository.Save();

            return NoContent();
        }

        [HttpGet("{id}/products", Name = nameof(GetFridgeProducts))]
        public IActionResult GetFridgeProducts([FromRoute(Name = "id")] Guid fridgeId)
        {
            var fridge = _repositoryManager.FridgeRepository.GetFridge(fridgeId);

            if (fridge == null)
                return NotFound();

            var productDTOs = fridge.Products.Select(p => _mapper.Map<StoredProductToReturn>(p));
            return Ok(productDTOs);
        }

        [HttpPost("{id}/products", Name = nameof(AddProductsToFridge))]
        public IActionResult AddProductsToFridge([FromRoute(Name = "id")] Guid fridgeId, [FromBody] IEnumerable<StoredProductToCreate> productDTOs)
        {
            if ((productDTOs == null) || !ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            Fridge fridge = _repositoryManager.FridgeRepository.GetFridge(fridgeId);

            if (fridge == null)
                return NotFound();

            var productsToAdd = new List<StoredProduct>();
            foreach (var productDTO in productDTOs)
            {
                Product product = _repositoryManager.ProductRepository.GetProduct((Guid)productDTO.ProductId);

                if (product == null)
                    return NotFound();

                StoredProduct toAdd = _mapper.Map<StoredProduct>(productDTO);
                toAdd.Product = product;
                productsToAdd.Add(toAdd);
            }

            fridge.Products.InsertRange(0, productsToAdd);
            _repositoryManager.FridgeRepository.Save();

            var productsToReturn = productsToAdd.Select(p => _mapper.Map<StoredProductToReturn>(p));
            return Created($"api/fridges/{fridgeId}/products", productsToReturn);
        }

        [HttpDelete("{id}/products", Name = nameof(DeleteProductFromFridge))]
        public IActionResult DeleteProductFromFridge([FromRoute(Name = "id")] Guid fridgeId, [FromBody] Guid storedProductId)
        {
            Fridge fridge = _repositoryManager.FridgeRepository.GetFridge(fridgeId);

            if (fridge == null)
                return NotFound();

            fridge.Products.RemoveAll(p => p.Id == storedProductId);
            _repositoryManager.FridgeRepository.Save();

            return NoContent();
        }

        [HttpPut("{id}/products", Name = nameof(UpdateProductsInFridge))]
        public IActionResult UpdateProductsInFridge([FromRoute(Name = "id")] Guid fridgeId, [FromBody] IEnumerable<StoredProductToUpdate> productDTOs)
        {
            if ((productDTOs == null) || !ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var fridge = _repositoryManager.FridgeRepository.GetFridge(fridgeId);

            if (fridge == null)
                return NotFound();

            var productsToUpdate = new List<StoredProduct>();
            var dtoList = productDTOs.ToList();

            foreach (var dto in dtoList)
            {
                var productToUpdate = fridge.Products.FirstOrDefault(p => p.Id == dto.Id);

                if (productToUpdate == null)
                    return NotFound();
                else
                    productsToUpdate.Add(productToUpdate);
            }

            for (int i = 0; i < dtoList.Count; ++i)
                _mapper.Map(dtoList[i], productsToUpdate[i]);

            _repositoryManager.FridgeRepository.Save();

            return NoContent();
        }

        [HttpPatch(Name = nameof(FillFridges))]
        public IActionResult FillFridges()
        {
            _repositoryManager.FridgeRepository.FillFridges();

            return Ok();
        }
    }
}
