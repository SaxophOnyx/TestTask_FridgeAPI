using AutoMapper;
using FridgeAPI.Entities.Models;
using FridgeAPI.Entities.DataTransferObjects.Request;
using FridgeAPI.Entities.DataTransferObjects.Response;

namespace FridgeAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateInput();
            CreateOutput();
        }


        private void CreateInput()
        {
            CreateMap<StoredProductToCreate, StoredProduct>();
            CreateMap<StoredProductToUpdate, StoredProduct>();
            CreateMap<FridgeToCreate, Fridge>();
            CreateMap<ProductToCreate, Product>();
        }

        private void CreateOutput()
        {
            CreateMap<StoredProduct, StoredProductToReturn>().ForMember(src => src.ProductName, opt => opt.MapFrom(p => p.Product.Name))
                                                             .ForMember(src => src.Id, opt => opt.MapFrom(p => p.Id))
                                                             .ForMember(src => src.ProductId, opt => opt.MapFrom(p => p.Product.Id));
            CreateMap<FridgeModel, FridgeModelToReturn>();
            CreateMap<Fridge, FridgeToReturn>();
            CreateMap<Product, ProductToReturn>();
        }
    }
}
