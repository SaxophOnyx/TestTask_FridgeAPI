namespace FridgeAPI.Entities.DatabaseAccess.Abstractions
{
    public interface IRepositoryManager
    {
        IFridgeRepository FridgeRepository { get; }

        IProductRepository ProductRepository { get; }

        IFridgeModelRepository FridgeModelRepository { get; }
    }
}
