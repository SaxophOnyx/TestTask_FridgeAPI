using System;

namespace FridgeAPI.Domain.Contracts.DataTransferObjects.Response
{
    public class StoredProductToReturn
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            StoredProductToReturn product = obj as StoredProductToReturn;

            if (product == null)
                return false;

            return Id.Equals(product.Id)
                && ProductId.Equals(product.ProductId)
                && ProductName.Equals(product.ProductName)
                && Quantity.Equals(product.Quantity);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ProductId, ProductName, Quantity);
        }
    }
}
