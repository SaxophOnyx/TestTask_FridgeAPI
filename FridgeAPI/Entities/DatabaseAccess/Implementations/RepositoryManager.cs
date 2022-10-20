using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using FridgeAPI.Entities.DatabaseAccess.Implmentations;
using FridgeAPI.Entities.Misc;

namespace FridgeAPI.Entities.DatabaseAccess.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        public IFridgeRepository FridgeRepository
        {
            get
            {
                if (_fridgeRepository == null)
                    _fridgeRepository = new FridgeRepository(_repositoryContext);

                return _fridgeRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);

                return _productRepository;
            }
        }

        public IFridgeModelRepository FridgeModelRepository
        {
            get
            {
                if (_fridgeModelRepository == null)
                    _fridgeModelRepository = new FridgeModelRepository(_repositoryContext);

                return _fridgeModelRepository;
            }
        }

        private RepositoryContext _repositoryContext;

        private IFridgeRepository _fridgeRepository;

        private IProductRepository _productRepository;

        private IFridgeModelRepository _fridgeModelRepository;


        public RepositoryManager(RepositoryContext context)
        {
            _repositoryContext = context;
        }
    }
}
