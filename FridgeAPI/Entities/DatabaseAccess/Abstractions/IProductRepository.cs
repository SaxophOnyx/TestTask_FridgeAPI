using FridgeAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FridgeAPI.Entities.DatabaseAccess.Abstractions
{
    public interface IProductRepository
    {
        Product GetProduct(Guid productId);

        IEnumerable<Product> GetProducts();

        void AddProduct(Product product);

        void DeleteProduct(Guid productId);

        int Save();
    }
}
