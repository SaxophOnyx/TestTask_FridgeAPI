using FridgeAPI.Infrastructure.Database.Misc;

namespace FridgeAPI.Infrastructure.Database.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly RepositoryContext _dbContext;


        protected RepositoryBase(RepositoryContext repositoryContext)
        {
            _dbContext = repositoryContext;
        }
    }
}
