using FridgeAPI.Domain.Contracts.Interfaces.Repositories;

namespace Tests.Mocks
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        public IFridgeRepository FridgeRepository => FridgeRepositoryMock;

        public IFridgeModelRepository FridgeModelRepository => FridgeModelRepositoryMock;

        public IProductRepository ProductRepository => ProductRepositoryMock;

        public FridgeRepositoryMock FridgeRepositoryMock { get; }

        public FridgeModelRepositoryMock FridgeModelRepositoryMock { get; }

        public ProductRepositoryMock ProductRepositoryMock { get; }


        public UnitOfWorkMock()
        {
            var frame = DbFrame.CreateFilled();

            FridgeRepositoryMock = new FridgeRepositoryMock(frame.Fridges);
            FridgeModelRepositoryMock = new FridgeModelRepositoryMock(frame.FridgeModels);
            ProductRepositoryMock = new ProductRepositoryMock(frame.Products);
        }
    }
}
