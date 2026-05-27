using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Tests.Validators
{
    public class CategoryValidatorTests
    {
        private readonly CategoryValidator _validator;

        public CategoryValidatorTests()
        {
            _validator = new CategoryValidator();
        }

        // ================= CATEGORY NAME TESTS =================

        [Fact]
        public void ValidateCategoryName_Should_Return_Valid_For_Proper_Name()
        {
            // Arrange
            string name = "Food";

            // Act
            var result =
                _validator.ValidateCategoryName(name);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateCategoryName_Should_Return_Invalid_For_Empty_Name()
        {
            // Arrange
            string name = "";

            // Act
            var result =
                _validator.ValidateCategoryName(name);

            // Assert
            Assert.False(result.IsValid);

            Assert.Equal(
                "Category name cannot be empty.",
                result.ErrorMessage);
        }

        [Fact]
        public void ValidateCategoryName_Should_Return_Invalid_For_Short_Name()
        {
            // Arrange
            string name = "ab";

            // Act
            var result =
                _validator.ValidateCategoryName(name);

            // Assert
            Assert.False(result.IsValid);

            Assert.Equal(
                "Category name must contain at least 3 characters.",
                result.ErrorMessage);
        }

        // ================= CATEGORY TYPE TESTS =================

        [Fact]
        public void ValidateCategoryType_Should_Return_Valid_For_Expense()
        {
            // Arrange
            int choice = 1;

            // Act
            var result =
                _validator.ValidateCategoryType(choice);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateCategoryType_Should_Return_Valid_For_Income()
        {
            // Arrange
            int choice = 2;

            // Act
            var result =
                _validator.ValidateCategoryType(choice);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateCategoryType_Should_Return_Invalid_For_Wrong_Choice()
        {
            // Arrange
            int choice = 5;

            // Act
            var result =
                _validator.ValidateCategoryType(choice);

            // Assert
            Assert.False(result.IsValid);

            Assert.Equal(
                "Please select valid category type.",
                result.ErrorMessage);
        }
    }
}