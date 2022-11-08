using FridgeAPI.Domain.Contracts.DataTransferObjects.Request;
using FridgeAPI.Domain.Contracts.DataTransferObjects.Response;
using FridgeAPI.Domain.Contracts.Interfaces.Repositories;
using FridgeAPI.Domain.Contracts.Interfaces.Services;
using FridgeAPI.Domain.Entities;
using FridgeAPI.Domain.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeAPI.Domain.Services
{
    public class ProductsService : ServiceBase, IProductsService
    {
        public ProductsService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }


        public async Task<IEnumerable<ProductToReturn>> AddProductsAsync(IEnumerable<ProductToCreate> products)
        {
            var entities = products.Select(pr => new Product()
            {
                Name = pr.Name,
                DefaultQuantity = pr.DefaultQuantity,
                Id = Guid.Empty
            });

            var created = await _unitOfWork.ProductRepository.AddProductsAsync(entities.ToList());
            await _unitOfWork.ProductRepository.SaveAsync();

            return created.Select(cr => cr.MapToResponse());
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _unitOfWork.ProductRepository.DeleteProductAsync(id);
            await _unitOfWork.ProductRepository.SaveAsync();
        }

        public async Task<IEnumerable<ProductToReturn>> GetProductsAsync()
        {
            var entities = await _unitOfWork.ProductRepository.GetProductsAsync();
            return entities.Select(e => e.MapToResponse());
        }
    }
}
