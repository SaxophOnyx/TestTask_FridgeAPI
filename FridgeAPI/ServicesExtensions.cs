using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using FridgeAPI.Entities.DatabaseAccess.Implementations;
using FridgeAPI.Entities.DatabaseAccess.Implmentations;
using FridgeAPI.Entities.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeAPI
{
    public static class ServicesExtensions
    {
        public static void ConfigureDatabaseConnection(this IServiceCollection services)
        {
            string connection = "data source=DESKTOP-7LS5A76;initial catalog=newfridgedb;trusted_connection=true";
            services.AddDbContext<RepositoryContext>(opt => opt.UseSqlServer(connection));
        }

        public static void ConfigureDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<DbContext, RepositoryContext>();
            services.AddScoped<IFridgeRepository, FridgeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFridgeModelRepository, FridgeModelRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();
        }
    }
}
