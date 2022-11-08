using System;

namespace FridgeAPI.Domain.Contracts.DataTransferObjects.Response
{
    public class ProductToReturn
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? DefaultQuantity { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            ProductToReturn product = obj as ProductToReturn;

            if (product == null)
                return false;

            return Id.Equals(product.Id)
                && Name.Equals(product.Name)
                && DefaultQuantity.Equals(product.DefaultQuantity);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, DefaultQuantity);
        }
    }
}
