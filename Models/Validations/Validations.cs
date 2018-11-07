using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Validations{
    
    public class IsUniqueEmail : ValidationAttribute{
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {           
            return ValidationResult.Success;
        }
    }
}