using PersonalFinanceManager.Models;

namespace PersonalFinanceManager.Validators
{
    public class ExpenseValidator
    {
        // ================= CATEGORY =================

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

        // ================= AMOUNT =================

        public ValidationResult ValidateAmount(decimal amount)
        {
            if (amount <= 0)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Amount must be greater than 0."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }

        // ================= DESCRIPTION =================

        public ValidationResult ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Description cannot be empty."
                };
            }

            if (description.Length < 3)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Description must contain at least 3 characters."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}