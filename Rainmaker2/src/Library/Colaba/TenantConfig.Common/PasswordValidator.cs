using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace TenantConfig.Common
{
    public class PasswordValidatorAttribute : ValidationAttribute
    {
        private static bool HasAtleastOneCharacter(string value)
        {
            return Regex.IsMatch(value,"[A-Za-z]+");
        }
        private static bool HasAtleastOneNumber(string value)
        {
            return Regex.IsMatch(value, "[0-9]+");
        }
        private static bool HasAtleastOneSpecialCharacter(string value)
        {
            return Regex.IsMatch(value, "[@!#$%+?=~]+");
        }
        private static bool IsRepeating(string value)
        {
            for(int i=0;i<value.Length-2;i++)
            {
                if (value[i] == value[i + 1] && value[i] == value[i + 2])
                    return true;
            }
            return false;
        }
        private static bool IsSequential(string value)
        {
            for (int i = 0; i < value.Length - 2; i++)
            {
                if (value[i+1] == value[i]+1 && value[i+2] == value[i]+2)
                    return true;
            }
            return false;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Password is required");
            string val = value.ToString();
            if (val.Length < 8)
                return new ValidationResult("Password should be atleast 8 charcters long");
            int score = 0;
            if (HasAtleastOneCharacter(val))
                score++;
            if (HasAtleastOneNumber(val))
                score++;
            if (HasAtleastOneSpecialCharacter(val))
                score++;
            if(score<2)
                return new ValidationResult("Password should contain two of following criteria. a) atleast one alphabet (case sensitive) b) atleast one digit c) atleast on special charcter among @!#$%+?=~");
            if(IsRepeating(val))
                return new ValidationResult("Password should not contain three repeating characters");
            if (IsSequential(val))
                return new ValidationResult("Password should not contain three sequential characters");
            return ValidationResult.Success;
        }
    }
}
