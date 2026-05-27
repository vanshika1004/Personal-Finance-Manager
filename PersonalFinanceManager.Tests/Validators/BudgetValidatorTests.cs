using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Tests.Validators
{
    public class BudgetValidatorTests
    {
        private readonly BudgetValidator _validator;

        public BudgetValidatorTests()
        {
            _validator = new BudgetValidator();
        }

        // ================= LIMIT AMOUNT TESTS =================

        [Fact]
        public void ValidateLimitAmount_Should_Return_Valid_For_Positive_Amount()
        {
            // Arrange
            decimal amount = 10000;

            // Act
            var result =
                _validator.ValidateLimitAmount(amount);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateLimitAmount_Should_Return_Invalid_For_Zero_Amount()
        {
            // Arrange
            decimal amount = 0;

            // Act
            var result =
                _validator.ValidateLimitAmount(amount);

            // Assert
            Assert.False(result.IsValid);

            Assert.Equal(
                "Budget amount must be greater than 0.",
                result.ErrorMessage);
        }

        // ================= MONTH TESTS =================

        [Fact]
        public void ValidateMonth_Should_Return_Valid_For_Valid_Month()
        {
            // Arrange
            int month = 5;

            // Act
            var result =
                _validator.ValidateMonth(month);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateMonth_Should_Return_Invalid_For_Month_Greater_Than_12()
        {
            // Arrange
            int month = 13;

            // Act
            var result =
                _validator.ValidateMonth(month);

            // Assert
            Assert.False(result.IsValid);

            Assert.Equal(
                "Month must be between 1 and 12.",
                result.ErrorMessage);
        }

        [Fact]
        public void ValidateMonth_Should_Return_Invalid_For_Month_Less_Than_1()
        {
            // Arrange
            int month = 0;

            // Act
            var result =
                _validator.ValidateMonth(month);

            // Assert
            Assert.False(result.IsValid);
        }

        // ================= YEAR TESTS =================

        [Fact]
        public void ValidateYear_Should_Return_Valid_For_Proper_Year()
        {
            // Arrange
            int year = 2025;

            // Act
            var result =
                _validator.ValidateYear(year);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateYear_Should_Return_Invalid_For_Invalid_Year()
        {
            // Arrange
            int year = 1500;

            // Act
            var result =
                _validator.ValidateYear(year);

            // Assert
            Assert.False(result.IsValid);

            Assert.Equal(
                "Invalid Year.",
                result.ErrorMessage);
        }
    }
}