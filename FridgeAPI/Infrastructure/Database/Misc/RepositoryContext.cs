using FridgeAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FridgeAPI.Infrastructure.Database.Misc
{
    public class RepositoryContext: DbContext
    {
        public DbSet<Fridge> Fridges { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<FridgeModel> FridgeModels { get; set; }

        public DbSet<StoredProduct> FridgeProducts { get; set; }


        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
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
    }
}
