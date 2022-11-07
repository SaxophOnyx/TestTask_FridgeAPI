using System;

namespace FridgeAPI.Domain.Entities
{
    public class StoredProduct
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            StoredProduct stored = obj as StoredProduct;

            if (stored == null)
                return false;

            return Id.Equals(stored.Id)
                && Product.Equals(stored.Product)
                && Quantity.Equals(stored.Quantity);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Product, Quantity);
        }
    }
}
