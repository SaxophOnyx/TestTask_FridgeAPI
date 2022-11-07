using FridgeAPI.Domain.Entities;
using System.Collections.Generic;

namespace Tests.Mocks
{
    public class DbFrame
    {
        public List<Fridge> Fridges { get; set; }

        public List<FridgeModel> FridgeModels { get; set; }

        public List<Product> Products { get; set; }


        public DbFrame()
        {
            Fridges = new List<Fridge>();
            FridgeModels = new List<FridgeModel>();
            Products = new List<Product>();
        }


        public static DbFrame CreateFilled()
        {
            var models = new List<FridgeModel>()
            {
                new FridgeModel()
                {
                    Id = Misc.GenerateGuid("1"),
                    Name = "Model_Name_1",
                    Year = 2001
                },

                new FridgeModel()
                {
                    Id = Misc.GenerateGuid("2"),
                    Name = "Model_Name_2",
                    Year = 2002
                },

                new FridgeModel()
                {
                    Id = Misc.GenerateGuid("3"),
                    Name = "Model_Name_3",
                    Year = 2003
                }
            };

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = Misc.GenerateGuid("4"),
                    Name = "Product_Name_1",
                    DefaultQuantity = 200
                },

                new Product()
                {
                    Id = Misc.GenerateGuid("5"),
                    Name = "Product_Name_2",
                    DefaultQuantity = 500
                },

                new Product()
                {
                    Id = Misc.GenerateGuid("6"),
                    Name = "Product_Name_3",
                    DefaultQuantity = 300
                }
            };

            var stored_1 = new List<StoredProduct>()
            {
                new StoredProduct()
                {
                    Id = Misc.GenerateGuid("7"),
                    Quantity = 1000,
                    Product = products[0]
                },

                new StoredProduct()
                {
                    Id = Misc.GenerateGuid("8"),
                    Quantity = 2000,
                    Product = products[1]
                }
            };

            var stored_2 = new List<StoredProduct>()
            {
                new StoredProduct()
                {
                    Id = Misc.GenerateGuid("9"),
                    Product = products[0],
                    Quantity = 214
                },

                new StoredProduct()
                {
                    Id = Misc.GenerateGuid("10"),
                    Product = products[1],
                    Quantity = 24
                },

                new StoredProduct()
                {
                    Id = Misc.GenerateGuid("11"),
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
                    Id = Misc.GenerateGuid("12"),
                    OwnerName = "Owner_Name_1",
                    Products = stored_1
                },

                new Fridge()
                {
                    Name = "Fridge_Name_2",
                    Model = models[1],
                    Id = Misc.GenerateGuid("13"),
                    OwnerName = "Owner_Name_2",
                    Products = stored_2
                }
            };

            return new DbFrame()
            {
                Fridges = fridges,
                FridgeModels = models,
                Products = products
            };
        }
    }
}
