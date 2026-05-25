using System.Text.RegularExpressions;

namespace PersonalFinanceManager.Validators
{
    public class AuthValidator
    {
        // ================= REGISTER VALIDATION =================

        public ValidationResult ValidateUsername(string username)
        {
            // USERNAME VALIDATION

            if (string.IsNullOrWhiteSpace(username))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Username cannot be empty."
                };
            }

            if (username.Length < 3)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Username must contain at least 3 characters."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }

        // EMAIL VALIDATION
        public ValidationResult ValidateEmail(string email)
        { 
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Email cannot be empty."
                };
            }

            if (!IsValidEmail(email))
            {
                return new ValidationResult
                {
                    IsValid = false,

                    ErrorMessage = "Invalid email format."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }

        // PASSWORD VALIDATION
        public ValidationResult ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Password cannot be empty."
                };
            }

            if (!IsStrongPassword(password))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage =
                        "Password must contain minimum 6 characters, 1 uppercase letter, 1 lowercase letter and 1 number."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }
        

        // ================= LOGIN VALIDATION =================

        public ValidationResult ValidateLogin(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Email cannot be empty."
                };
            }

            if (!IsValidEmail(email))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Invalid email format."
                };
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Password cannot be empty."
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }

        // ================= EMAIL VALIDATION =================

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        // ================= PASSWORD VALIDATION =================

        private bool IsStrongPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}