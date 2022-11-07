using FridgeAPI.Domain.Contracts.Interfaces.Repositories;

namespace FridgeAPI.Domain.Services
{
    public abstract class ServiceBase
    {
        protected readonly IUnitOfWork _unitOfWork;


        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
