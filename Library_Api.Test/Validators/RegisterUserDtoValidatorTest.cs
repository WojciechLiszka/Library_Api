using Library_Api.Entity;
using Library_Api.Models.Validators;
using Library_Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentValidation.TestHelper;

namespace Library_Api.Test.Validators
{
    public class RegisterUserDtoValidatorTest
    {
        public class RegisterUserDtoValidatorTests
        {
            private readonly LibraryDbContext _dbContext;

            public RegisterUserDtoValidatorTests()
            {
                var builder = new DbContextOptionsBuilder<LibraryDbContext>();
                builder.UseInMemoryDatabase("TestDb");

                _dbContext = new LibraryDbContext(builder.Options);
            }

            [Fact]
            public void Validate_ForValidModel_ReturnsSuccess()
            {
                // arrange

                var model = new RegisterUserDto()
                {
                    Email = "test@test.com",
                    Password = "pass123",
                    ConfirmPassword = "pass123",
                };

                var validator = new RegisterUserDtoValidator(_dbContext);

                // act
                var result = validator.TestValidate(model);

                // assert

                result.ShouldNotHaveAnyValidationErrors();
            }

            [Fact]
            public void Validate_ForInvalidModel_ReturnsFailure()
            {
                // arrange

                var model = new RegisterUserDto()
                {
                    Email = "test2@test.com",
                    Password = "pass123",
                    ConfirmPassword = "passs123",
                };

                var validator = new RegisterUserDtoValidator(_dbContext);

                // act
                var result = validator.TestValidate(model);

                // assert

                result.ShouldHaveAnyValidationError();
            }
        }
    }
}
