using FridgeAPI.Entities.DatabaseAccess.Abstractions;

namespace Tests.Mocks
{
    internal class RepositoryManagerMock : IRepositoryManager
    {
        public IFridgeRepository FridgeRepository
        {
            get { return _friges; }
        }

        public IProductRepository ProductRepository
        {
            get { return _products; }
        }

        public IFridgeModelRepository FridgeModelRepository
        {
            get { return _models; }
        }

        private IFridgeRepository _friges;

        private IProductRepository _products;

        private IFridgeModelRepository _models;


        public RepositoryManagerMock(IFridgeRepository friges, IProductRepository products, IFridgeModelRepository models)
        {
            _friges = friges;
            _products = products;
            _models = models;
        }
    }
}
