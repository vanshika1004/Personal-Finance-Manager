using PersonalFinanceManager.Models;

namespace PersonalFinanceManager.Validators
{
    public class BudgetValidator
    {

        public ValidationResult ValidateCategory(Category? category)
        {
            if (category == null)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Invalid Category."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }

        public ValidationResult ValidateLimitAmount(decimal limitAmount)
        {
            if (limitAmount <= 0)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Budget amount must be greater than 0."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }

        public ValidationResult ValidateMonth(
            int month)
        {
            if (month < 1 || month > 12)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Month must be between 1 and 12."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }

        public ValidationResult ValidateYear(
            int year)
        {
            if (year < 2000 || year > 2100)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Invalid Year."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}