using System;
using System.ComponentModel.DataAnnotations;
using FridgeAPI.Attributes;

namespace FridgeAPI.DataTransferObjects.DataTransferObjects.Request
{
    public class FridgeToCreate
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Owner name is a required field")]
        public string? OwnerName { get; set; }

        [GuidValidation]
        public Guid? ModelId { get; set; }
    }
}
