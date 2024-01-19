using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.validators
{
    public class FirstCapitalLetter : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;

            string firstLetter = value.ToString()[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
                return new ValidationResult("La primera letra debe ser mayúscula");
            return ValidationResult.Success;
        }
    }
}