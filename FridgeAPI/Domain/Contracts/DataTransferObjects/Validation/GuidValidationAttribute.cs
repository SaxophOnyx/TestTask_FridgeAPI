using System.ComponentModel.DataAnnotations;
using System;

namespace FridgeAPI.Domain.Contracts.DataTransferObjects.Validation
{
    public class GuidValidation : ValidationAttribute
    {
        public override bool RequiresValidationContext => true;


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult($"{validationContext?.DisplayName} is a required field");

            try
            {
                Guid g = (Guid)value;

                if (g != Guid.Empty)
                    return ValidationResult.Success;
                else
                    return new ValidationResult($"{validationContext?.DisplayName} must be a guid not equal to the default value");

            }
            catch
            {
                return new ValidationResult($"{validationContext?.DisplayName} is invalid");
            }
        }
    }
}
