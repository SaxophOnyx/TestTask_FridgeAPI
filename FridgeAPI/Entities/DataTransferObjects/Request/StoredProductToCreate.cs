using FridgeAPI.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace FridgeAPI.Entities.DataTransferObjects.Request
{
    public class StoredProductToCreate
    {
        [GuidValidation]
        public Guid? ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is a required field")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int? Quantity { get; set; }
    }
}