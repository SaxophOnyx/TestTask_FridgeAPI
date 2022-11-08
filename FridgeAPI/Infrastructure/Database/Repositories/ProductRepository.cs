using FridgeAPI.Domain.Contracts.Exceptions;
using FridgeAPI.Domain.Contracts.Interfaces.Repositories;
using FridgeAPI.Domain.Entities;
using FridgeAPI.Infrastructure.Database.Misc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeAPI.Infrastructure.Database.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {

        }


        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<List<Product>> AddProductsAsync(List<Product> products)
        {
            await _dbContext.Products.AddRangeAsync(products);
            return products;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (entity != null)
                _dbContext.Products.Remove(entity);
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            try
            {
                return await _dbContext.Products.SingleAsync(p => p.Id == id);
            }
            catch(Exception)
            {
                throw new EntityNotFoundException(id, $"Product with id {id} not found");
            }
        }
    }
}
