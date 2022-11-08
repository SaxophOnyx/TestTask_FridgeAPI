namespace FridgeAPI.Domain.Contracts.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public IFridgeRepository FridgeRepository { get; }

        public IFridgeModelRepository FridgeModelRepository { get; }

        public IProductRepository ProductRepository { get; }
    }
}
