using System;
using System.ComponentModel.DataAnnotations;

namespace FridgeAPI.Entities.DataTransferObjects.Request
{
    public class ProductToCreate
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Default Quantity is a required field")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int? DefaultQuantity { get; set; }
    }
}
