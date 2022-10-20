using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using FridgeAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Mocks
{
    internal class ProductRepositoryMock : IProductRepository
    {
        private List<Product> _products;


        public ProductRepositoryMock(List<Product> products)
        {
            _products = products;
        }


        public Product GetProduct(Guid productId)
        {
            return _products.Find(p => p.Id == productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _products;
        }

        public List<Product> GetProductCollectionDeepcopy()
        {
            var list = new List<Product>();

            foreach (var p in _products)
            {
                var product = new Product()
                {
                    Id = p.Id,
                    Name = p.Name,
                    DefaultQuantity = p.DefaultQuantity
                };

                list.Add(product);
            }

            return list;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void DeleteProduct(Guid productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
                _products.Remove(product);
        }

        public int Save()
        {
            return 0;
        }
    }
}
