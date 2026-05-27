using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Tests.Validators
{
    public class IncomeValidatorTests
    {
        private readonly IncomeValidator _validator;

        public IncomeValidatorTests()
        {
            _validator = new IncomeValidator();
        }

        // ================= AMOUNT TESTS =================

        [Fact]
        public void ValidateAmount_Should_Return_Valid_For_Positive_Amount()
        {
            // Arrange
            decimal amount = 5000;

            // Act
            var result =
                _validator.ValidateAmount(amount);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateAmount_Should_Return_Invalid_For_Zero_Amount()
        {
            // Arrange
            decimal amount = 0;

            // Act
            var result =
                _validator.ValidateAmount(amount);

            // Assert
            Assert.False(result.IsValid);

            Assert.Equal(
                "Amount must be greater than 0.",
                result.ErrorMessage);
        }

        [Fact]
        public void ValidateAmount_Should_Return_Invalid_For_Negative_Amount()
        {
            // Arrange
            decimal amount = -1000;

            // Act
            var result =
                _validator.ValidateAmount(amount);

            // Assert
            Assert.False(result.IsValid);
        }

        // ================= SOURCE TESTS =================

        [Fact]
        public void ValidateSource_Should_Return_Valid_For_Proper_Source()
        {
            // Arrange
            string source = "Salary";

            // Act
            var result =
                _validator.ValidateSource(source);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateSource_Should_Return_Invalid_For_Empty_Source()
        {
            // Arrange
            string source = "";

            // Act
            var result =
                _validator.ValidateSource(source);

            // Assert
            Assert.False(result.IsValid);

            Assert.Equal(
                "Source cannot be empty.",
                result.ErrorMessage);
        }

        [Fact]
        public void ValidateSource_Should_Return_Invalid_For_Short_Source()
        {
            // Arrange
            string source = "ab";

            // Act
            var result =
                _validator.ValidateSource(source);

            // Assert
            Assert.False(result.IsValid);

            Assert.Equal(
                "Source must contain at least 3 characters.",
                result.ErrorMessage);
        }
    }
}