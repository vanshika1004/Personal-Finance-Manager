using PersonalFinanceManager.Enums;

namespace PersonalFinanceManager.Validators
{
    public class CategoryValidator
    {
        public ValidationResult ValidateCategoryName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Category name cannot be empty."
                };
            }

            if (name.Length < 3)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Category name must contain at least 3 characters."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }

        public ValidationResult ValidateCategoryType(int choice)
        {
            if (choice != 1 && choice != 2)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Please select valid category type."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}