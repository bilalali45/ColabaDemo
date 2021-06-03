using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace LoanApplication.Model.CustomAttributes
{
    public class DOBDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dt;
                if(!DateTime.TryParse(Convert.ToString(value), out dt))
                {
                    return new ValidationResult("Invalid DOB");
                }

                if (dt >= DateTime.UtcNow)
                {
                    if (dt > DateTime.UtcNow)
                    {
                        return new ValidationResult("DOB cannot be future date.");
                    }

                    if (dt == DateTime.UtcNow)
                    {
                        return new ValidationResult("DOB cannot be today's date.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
