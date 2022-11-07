using FridgeAPI.Domain.Contracts.Interfaces.Repositories;
using FridgeAPI.Infrastructure.Database.Repositories;

namespace FridgeAPI.Infrastructure.Database.Misc
{
    public class UnitOfWork : IUnitOfWork
    {
        public IFridgeRepository FridgeRepository { get; }

        public IFridgeModelRepository FridgeModelRepository { get; }

        public IProductRepository ProductRepository { get; }

        private readonly RepositoryContext _repositoryContext;


        public UnitOfWork(RepositoryContext context)
        {
            _repositoryContext = context;
            FridgeRepository = new FridgeRepository(_repositoryContext);
            FridgeModelRepository = new FridgeModelRepository(_repositoryContext);
            ProductRepository = new ProductRepository(_repositoryContext);
        }
    }
}
