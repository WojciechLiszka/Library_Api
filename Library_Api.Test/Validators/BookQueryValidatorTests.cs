using FluentValidation.TestHelper;
using Library_Api.Models;
using Library_Api.Models.Validators;
using Xunit;

namespace Library_Api.Test.Validators
{
    public class BookQueryValidatorTests
    {
        [Fact]
        public void Validate_ForValidModel_ReturnsSuccess()
        {
            // arrange
            var model = new BookQuery()
            {
                SearchPhrase = "Test",
                PageNumber = 1,
                PageSize = 5,
                SortBy = "Tittle",
                SortDirection = SortDirection.ASC
            };
            var validator = new BookQueryValidator();
            // act
            var result = validator.TestValidate(model);
            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact]
        public void Validate_ForInvalidModel_ReturnsFailure()
        {
            // arrange
            var model = new BookQuery()
            {
                SearchPhrase = "Test",
                PageNumber = 1,
                PageSize = 7,
                SortBy = "Tittle",
                SortDirection = SortDirection.ASC
            };
            var validator= new BookQueryValidator();
            // act
            var result = validator.TestValidate(model);
            // assert
            result.ShouldHaveAnyValidationError();
        }
    }
}