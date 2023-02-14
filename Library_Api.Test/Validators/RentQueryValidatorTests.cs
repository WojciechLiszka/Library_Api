using Library_Api.Models.Validators;
using Library_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentValidation.TestHelper;

namespace Library_Api.Test.Validators
{
    public class RentQueryValidatorTests
    {
        [Fact]
        public void Validate_ForValidModel_ReturnsSuccess()
        {
            // arrange
            var model = new RentQuery()
            {
                PageNumber = 1,
                PageSize = 5
            };
            var validator = new RentQueryValidator();
            // act
            var result = validator.TestValidate(model);
            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ForInvalidModel_ReturnsFailure()
        {
            // arrange
            var model = new RentQuery()
            {
                PageNumber = 1,
                PageSize = 7,
            };
            var validator = new RentQueryValidator();
            // act
            var result = validator.TestValidate(model);
            // assert
            result.ShouldHaveAnyValidationError();
        }
        
        
    }
}
