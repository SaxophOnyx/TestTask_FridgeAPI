using FridgeAPI.Domain.Contracts.DataTransferObjects.Response;
using FridgeAPI.Domain.Entities;

namespace FridgeAPI.Domain.Misc
{
    public static class ResponseMappingExtensions
    {
        public static FridgeToReturn MapToResponse(this Fridge fridge)
        {
            return new FridgeToReturn()
            {
                Id = fridge.Id,
                Name = fridge.Name,
                OwnerName = fridge.OwnerName,
                Model = fridge.Model.MapToResponse()
            };
        }

        public static FridgeModelToReturn MapToResponse(this FridgeModel model)
        {
            return new FridgeModelToReturn()
            {
                Id = model.Id,
                Name = model.Name,
                Year = model.Year
            };
        }

        public static StoredProductToReturn MapToResponse(this StoredProduct product)
        {
            return new StoredProductToReturn()
            {
                Id = product.Id,
                ProductName = product.Product.Name,
                ProductId = product.Product.Id,
                Quantity = product.Quantity
            };
        }

        public static ProductToReturn MapToResponse(this Product product)
        {
            return new ProductToReturn()
            {
                Name = product.Name,
                Id = product.Id,
                DefaultQuantity = product.DefaultQuantity
            };
        }
    }
}
