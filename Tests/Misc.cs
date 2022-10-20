using FridgeAPI.Entities.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using Tests.Mocks;

namespace Tests
{
    public static class Misc
    {
        public static Guid GetGuid(string s)
        {
            return string.IsNullOrEmpty(s)
                ? Guid.Empty
                : new Guid(MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(s)));
        }

        public static DatabaseFrame GenerateFrame()
        {
            var models = new List<FridgeModel>()
            {
                new FridgeModel()
                {
                    Id = GetGuid("1"),
                    Name = "Model_Name_1",
                    Year = 2001
                },

                new FridgeModel()
                {
                    Id = GetGuid("2"),
                    Name = "Model_Name_2",
                    Year = 2002
                },

                new FridgeModel()
                {
                    Id = GetGuid("3"),
                    Name = "Model_Name_3",
                    Year = 2003
                }
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = GetGuid("4"),
                    Name = "Product_Name_1",
                    DefaultQuantity = 200
                },

                new Product()
                {
                    Id = GetGuid("5"),
                    Name = "Product_Name_2",
                    DefaultQuantity = 500
                },

                new Product()
                {
                    Id = GetGuid("6"),
                    Name = "Product_Name_3",
                    DefaultQuantity = 300
                }
            };

            var stored_1 = new List<StoredProduct>()
            {
                new StoredProduct()
                {
                    Id = GetGuid("7"),
                    Quantity = 1000,
                    Product = products[0]
                },

                new StoredProduct()
                {
                    Id = GetGuid("8"),
                    Quantity = 2000,
                    Product = products[1]
                }
            };

            var stored_2 = new List<StoredProduct>()
            {
                new StoredProduct()
                {
                    Id = GetGuid("9"),
                    Product = products[0],
                    Quantity = 214
                },

                new StoredProduct()
                {
                    Id = GetGuid("10"),
                    Product = products[1],
                    Quantity = 24
                },

                new StoredProduct()
                {
                    Id = GetGuid("11"),
                    Product = products[2],
                    Quantity = 2140
                }
            };

            var fridges = new List<Fridge>()
            {
                new Fridge()
                {
                    Name = "Fridge_Name_1",
                    Model = models[0],
                    Id = GetGuid("12"),
                    OwnerName = "Owner_Name_1",
                    Products = stored_1
                },

                new Fridge()
                {
                    Name = "Fridge_Name_2",
                    Model = models[1],
                    Id = GetGuid("13"),
                    OwnerName = "Owner_Name_2",
                    Products = stored_2
                }
            };

            return new DatabaseFrame()
            {
                Fridges = fridges,
                Products = products,
                FridgeModels = models
            };
        }
    }
}
