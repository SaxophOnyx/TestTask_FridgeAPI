using Microsoft.EntityFrameworkCore;
using FridgeAPI.Entities.Models;
using System;
using System.Collections.Generic;

namespace FridgeAPI.Entities.Misc
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Fridge> Fridges { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<FridgeModel> FridgeModels { get; set; }

        public DbSet<StoredProduct> FridgeProducts { get; set; }


        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            //FillDatabase();
            //SaveChanges();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var fridgeModelBuilder = modelBuilder.Entity<FridgeModel>();
            fridgeModelBuilder.ToTable("fridge_model");
            fridgeModelBuilder.HasKey(x => x.Id);
            fridgeModelBuilder.Property(x => x.Id).HasColumnName("id");
            fridgeModelBuilder.Property(x => x.Name).HasColumnName("name").IsRequired();
            fridgeModelBuilder.Property(x => x.Year).HasColumnName("year").IsRequired(false);

            var productBuilder = modelBuilder.Entity<Product>();
            productBuilder.ToTable("product");
            productBuilder.HasKey(x => x.Id);
            productBuilder.Property(x => x.Id).HasColumnName("id");
            productBuilder.Property(x => x.Name).HasColumnName("name").IsRequired();
            productBuilder.Property(x => x.DefaultQuantity).HasColumnName("default_quantity").IsRequired(false);

            var fridgeProductBuilder = modelBuilder.Entity<StoredProduct>();
            fridgeProductBuilder.ToTable("stored_product");
            fridgeProductBuilder.HasKey(x => x.Id);
            fridgeProductBuilder.Property(x => x.Id).HasColumnName("id");
            fridgeProductBuilder.Property(x => x.Quantity).HasColumnName("quantity").IsRequired();
            fridgeProductBuilder.HasOne(x => x.Product).WithMany().HasForeignKey("product_id").IsRequired().OnDelete(DeleteBehavior.Cascade); ;

            var fridgeBuilder = modelBuilder.Entity<Fridge>();
            fridgeBuilder.ToTable("fridge");
            fridgeBuilder.HasKey(x => x.Id);
            fridgeBuilder.Property(x => x.Id).HasColumnName("id");
            fridgeBuilder.Property(x => x.Name).HasColumnName("name").IsRequired();
            fridgeBuilder.Property(x => x.OwnerName).HasColumnName("owner_name").IsRequired(false);
            fridgeBuilder.HasOne(x => x.Model).WithMany().HasForeignKey("model_id").IsRequired();
            fridgeBuilder.HasMany(x => x.Products).WithOne().HasForeignKey("fridge_id").IsRequired().OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        private void FillDatabase()
        {
            Guid g1 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
            Guid g2 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2);

            FridgeModel[] fridgeModels = new FridgeModel[]
            {
                new FridgeModel() { Id = g1, Name = "Model_Name_1", Year = 2000 },
                new FridgeModel() { Id = g2, Name = "Model_Name_2", Year = 2001 }
            };

            FridgeModels.AddRange(fridgeModels);


            Guid g3 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3);
            Guid g4 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4);
            Guid g5 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5);

            Product[] products = new Product[]
            {
                new Product() { Id = g3, Name = "Product_Name_1", DefaultQuantity = 200 },
                new Product() { Id = g4, Name = "Product_Name_2", DefaultQuantity = 300 },
                new Product() { Id = g5, Name = "Product_Name_3", DefaultQuantity = 250 }
            };

            Products.AddRange(products);


            Guid g6 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6);
            Guid g7 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7);
            Guid g8 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8);
            Guid g9 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9);

            StoredProduct[] fridgeProducts = new StoredProduct[]
            {
                new StoredProduct() { Id = g6, Product = products[0], Quantity = 350 },
                new StoredProduct() { Id = g7, Product = products[1], Quantity = 450 },
                new StoredProduct() { Id = g8, Product = products[1], Quantity = 700 },
                new StoredProduct() { Id = g9, Product = products[2], Quantity = 100 }
            };

            Guid g10 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10);
            Guid g11 = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11);

            Fridge[] fridges = new Fridge[]
            {
                new Fridge()
                {
                    Id = g10, Name = "Fridge_Name_1", OwnerName = "Owner_Name_1", Model = fridgeModels[0],
                    Products = new List<StoredProduct>()
                    {
                        fridgeProducts[0],
                        fridgeProducts[1]
                    }
                },
                new Fridge()
                {
                    Id = g11, Name = "Fridge_Name_2", OwnerName = "Owner_Name_2", Model = fridgeModels[1],
                    Products = new List<StoredProduct>()
                    {
                        fridgeProducts[2],
                        fridgeProducts[3]
                    }
                }
            };

            Fridges.AddRange(fridges);
        }
    }
}
