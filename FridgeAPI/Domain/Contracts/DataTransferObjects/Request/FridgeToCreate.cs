using System.ComponentModel.DataAnnotations;
using System;
using FridgeAPI.Domain.Contracts.DataTransferObjects.Validation;

namespace FridgeAPI.Domain.Contracts.DataTransferObjects.Request
{
    public class FridgeToCreate
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Owner name is a required field")]
        public string OwnerName { get; set; }

        [GuidValidation]
        public Guid? ModelId { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            FridgeToCreate fridge = obj as FridgeToCreate;

            if (fridge == null)
                return false;

            return Name.Equals(fridge.Name)
                && OwnerName.Equals(fridge.OwnerName)
                && ModelId.Equals(fridge.ModelId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, OwnerName, ModelId);
        }
    }
}
