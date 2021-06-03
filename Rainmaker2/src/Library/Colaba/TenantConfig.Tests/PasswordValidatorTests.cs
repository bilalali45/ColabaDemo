using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common;
using Xunit;

namespace TenantConfig.Tests
{
    public class PasswordValidatorTests
    {
        [Fact]
        public async Task PasswordValidatorNull()
        {
            PasswordValidatorAttribute validator = new PasswordValidatorAttribute();
            var res = validator.GetValidationResult(null, new ValidationContext(this));
            Assert.NotEqual(ValidationResult.Success, res);
        }
        [Fact]
        public async Task PasswordValidatorLength()
        {
            PasswordValidatorAttribute validator = new PasswordValidatorAttribute();
            var res = validator.GetValidationResult("Test@12", new ValidationContext(this));
            Assert.NotEqual(ValidationResult.Success,res);
        }
        [Fact]
        public async Task PasswordValidatorCheckScore()
        {
            PasswordValidatorAttribute validator = new PasswordValidatorAttribute();
            var res = validator.GetValidationResult("aaaaaaaa", new ValidationContext(this));
            Assert.NotEqual(ValidationResult.Success, res);
        }
        [Fact]
        public async Task PasswordValidatorCheckRepeating()
        {
            PasswordValidatorAttribute validator = new PasswordValidatorAttribute();
            var res = validator.GetValidationResult("Test@111", new ValidationContext(this));
            Assert.NotEqual(ValidationResult.Success, res);
        }
        [Fact]
        public async Task PasswordValidatorCheckSequential()
        {
            PasswordValidatorAttribute validator = new PasswordValidatorAttribute();
            var res = validator.GetValidationResult("Test@123", new ValidationContext(this));
            Assert.NotEqual(ValidationResult.Success, res);
        }

        [Fact]
        public async Task PasswordValidatorCheckValid()
        {
            PasswordValidatorAttribute validator = new PasswordValidatorAttribute();
            var res = validator.GetValidationResult("Test@124", new ValidationContext(this));
            Assert.Equal(ValidationResult.Success, res);
        }
    }
}
