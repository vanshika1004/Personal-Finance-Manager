using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Tests.Validators
{
    public class AuthValidatorTests
    {
        private readonly AuthValidator _validator;

        public AuthValidatorTests()
        {
            _validator = new AuthValidator();
        }

        [Fact]
        public void ValidateEmail_Should_Return_Valid_For_Correct_Email()
        {
            // Arrange
            string email = "vanshika@gmail.com";

            // Act
            var result =
                _validator.ValidateEmail(email);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateEmail_Should_Return_Invalid_For_Wrong_Email()
        {
            // Arrange
            string email = "vanshika@";

            // Act
            var result =
                _validator.ValidateEmail(email);

            // Assert
            Assert.False(result.IsValid);

            Assert.Equal(
                "Invalid email format.",
                result.ErrorMessage);
        }

        [Fact]
        public void ValidatePassword_Should_Return_Valid_For_Strong_Password()
        {
            string password = "Vanshika123";

            var result =
                _validator.ValidatePassword(password);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidatePassword_Should_Return_Invalid_For_Weak_Password()
        {
            string password = "abc123";

            var result =
                _validator.ValidatePassword(password);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ValidateUsername_Should_Return_Invalid_For_Short_Name()
        {
            string username = "ab";

            var result =
                _validator.ValidateUsername(username);

            Assert.False(result.IsValid);
        }
    }
}