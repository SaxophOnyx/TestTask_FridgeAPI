using FridgeAPI.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace FridgeAPI.DataTransferObjects.DataTransferObjects.Request
{
    public class StoredProductToUpdate
    {
        [GuidValidation]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Quantity is a required field")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int? Quantity { get; set; }
    }
}
