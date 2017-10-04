using System.ComponentModel.DataAnnotations;

using projectapi.Models;

namespace projectapi.ModelValidation
{
    public class MinimumItemLevelAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            NewItem item = (NewItem)validationContext.ObjectInstance;

            if(item._Type == "Sword" && item._Level < 20)
            {
                return new ValidationResult("The level requirement for sword is 20\n");
            }
            return ValidationResult.Success;
        }
    }
}