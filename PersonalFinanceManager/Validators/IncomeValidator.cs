using PersonalFinanceManager.Models;

namespace PersonalFinanceManager.Validators
{
    public class IncomeValidator
    {
        // ================= CATEGORY VALIDATION =================

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

        // ================= AMOUNT VALIDATION =================

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

        // ================= SOURCE VALIDATION =================

        public ValidationResult ValidateSource(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Source cannot be empty."
                };
            }

            if (source.Length < 3)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Source must contain at least 3 characters."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}