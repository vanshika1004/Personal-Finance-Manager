using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Tests.Validators
{
    public class ExpenseValidatorTests
    {
        private readonly ExpenseValidator _validator;

        public ExpenseValidatorTests()
        {
            _validator = new ExpenseValidator();
        }

        [Fact]
        public void ValidateAmount_Should_Return_Valid_For_Positive_Amount()
        {
            decimal amount = 500;

            var result =
                _validator.ValidateAmount(amount);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateAmount_Should_Return_Invalid_For_Zero_Amount()
        {
            decimal amount = 0;

            var result =
                _validator.ValidateAmount(amount);

            Assert.False(result.IsValid);

            Assert.Equal(
                "Amount must be greater than 0.",
                result.ErrorMessage);
        }

        [Fact]
        public void ValidateDescription_Should_Return_Invalid_For_Empty_Description()
        {
            string description = "";

            var result =
                _validator.ValidateDescription(description);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ValidateDescription_Should_Return_Valid_For_Proper_Description()
        {
            string description = "Food Expense";

            var result =
                _validator.ValidateDescription(description);

            Assert.True(result.IsValid);
        }
    }
}