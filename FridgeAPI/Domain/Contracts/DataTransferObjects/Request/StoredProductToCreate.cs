using FridgeAPI.Domain.Contracts.DataTransferObjects.Validation;
using System.ComponentModel.DataAnnotations;
using System;

namespace FridgeAPI.Domain.Contracts.DataTransferObjects.Request
{
    public class StoredProductToCreate
    {
        [GuidValidation]
        public Guid? ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is a required field")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int? Quantity { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            StoredProductToCreate stored = obj as StoredProductToCreate;

            if (stored == null)
                return false;

            return ProductId.Equals(stored.ProductId)
                && Quantity.Equals(stored.Quantity);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductId, Quantity);
        }
    }
}
