using FridgeAPI.Entities.Models;
using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FridgeAPI.Entities.DatabaseAccess.Implmentations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) 
            : base(context)
        {

        }


        public Product GetProduct(Guid productId)
        {
            return GetProducts().FirstOrDefault(p => p.Id == productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return GetEntities().AsEnumerable();
        }

        public void AddProduct(Product product)
        {
            AddEntity(product);
        }

        public void DeleteProduct(Guid productId)
        {
            var product = GetProduct(productId);

            if (product != null)
                DeleteEntity(product);
        }
    }
}
