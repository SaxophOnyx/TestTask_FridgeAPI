using FridgeAPI.Domain.Contracts.Interfaces.Repositories;
using FridgeAPI.Domain.Contracts.Interfaces.Services;
using FridgeAPI.Domain.Services;
using FridgeAPI.Infrastructure.Database.Misc;
using FridgeAPI.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FridgeAPI
{
    public static class StartupExtensions
    {
        public static void ConfigureDatabaseConnection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RepositoryContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IFridgeRepository, FridgeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFridgeModelRepository, FridgeModelRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFridgeModelsService, FridgeModelsService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IFridgesService, FridgesService>();
        }
    }
}