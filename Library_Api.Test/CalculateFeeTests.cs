using Library_Api.Entity;
using Library_Api.Features.AdminPanel;
using Xunit;
using FluentAssertions;
using Library_Api.Features.RentService.Command.Helper;

namespace Library_Api.Features.RentService.Tests
{
    public class CalculateFeeTests
    {
        [Fact]
        public void Calculate_WhenReturnDateIsNullAndEndsDateIsInTheFuture_ShouldReturnZero()
        {
            // Arrange
            var rent = new Rent { Ends = DateTime.Now.AddDays(10), ReturnDate = null };

            // Act
            var result = CalculateFee.Calculate(rent);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void Calculate_WhenReturnDateIsNullAndEndsDateIsInThePast_ShouldReturnLateFee()
        {
            // Arrange
            var rent = new Rent { Ends = DateTime.Now.AddDays(-10), ReturnDate = null };
            double expectedResult = 10 * ApiConfiguration.GetInstance().Latefee;

            // Act
            var result = CalculateFee.Calculate(rent);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Calculate_WhenReturnDateIsNotNull_ShouldReturnLateFee()
        {
            // Arrange
            var rent = new Rent { Ends = DateTime.Now.AddDays(-10), ReturnDate = DateTime.Now.AddDays(-5) };
            var expectedResult = 5 * ApiConfiguration.GetInstance().Latefee;

            // Act
            double result = CalculateFee.Calculate(rent);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}