using System;
using System.ComponentModel.DataAnnotations;

namespace FridgeAPI.Domain.Contracts.DataTransferObjects.Request
{
    public class ProductToCreate
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Default Quantity is a required field")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int? DefaultQuantity { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            ProductToCreate product = obj as ProductToCreate;

            if (product == null)
                return false;

            return Name.Equals(product.Name)
                && DefaultQuantity.Equals(product.DefaultQuantity);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DefaultQuantity);
        }
    }
}
