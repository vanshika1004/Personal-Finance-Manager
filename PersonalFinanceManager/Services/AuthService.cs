using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceManager.Data;
using PersonalFinanceManager.Helpers;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly AuthValidator _authValidator;
        public AuthService(AppDbContext context, AuthValidator authValidator)
        {
            _context = context;
            _authValidator = authValidator;
        }

        public void Register()
        {
            ConsoleHelper.ShowTitle("=======Register=======");

            string username = GetValidUsername();
            string email = GetValidEmail();
            string password = GetValidPassword();

            var existingUser = _context.Users.FirstOrDefault(x => x.Email == email);

            if (existingUser != null)
            {
                ConsoleHelper.ShowError("User already exists with this email.");
                ConsoleHelper.Pause();
                return;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            User user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            ConsoleHelper.ShowSuccess("Registration successful");
            ConsoleHelper.Pause();
        }

        public User? Login()
        {
            ConsoleHelper.ShowTitle("====Login====");

            string email = GetValidLoginEmail();
            string password = GetValidLoginPassword();
            Console.WriteLine(password);

            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                ConsoleHelper.ShowError("\nUser does not Exist.");
                ConsoleHelper.Pause();
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isPasswordValid)
            {
                ConsoleHelper.ShowError("\nIncorrect Password.");
                ConsoleHelper.Pause();
                return null;
            }

            ConsoleHelper.ShowSuccess($"\nWelcome {user.Username}");
            Console.ReadKey();
            return user;

        }

        private string GetValidUsername()
        {
            while (true)
            {
                string username = InputHelper.GetRequiredStringInput("Enter Username: ");
                var validationResult = _authValidator.ValidateUsername(username);
                if (validationResult.IsValid)
                {
                    return username;
                }

                ConsoleHelper.ShowError(validationResult.ErrorMessage);
                ConsoleHelper.Pause();
            }
        }

        private string GetValidEmail()
        {
            while (true)
            {
                string email = InputHelper.GetRequiredStringInput("Enter Email: ");
                var validationResult = _authValidator.ValidateEmail(email);
                if (validationResult.IsValid)
                {
                    return email;
                }

                ConsoleHelper.ShowError(validationResult.ErrorMessage);
                ConsoleHelper.Pause();
            }
        }

        private string GetValidPassword()
        {
            while (true)
            {
                string password = InputHelper.GetRequiredStringInput("Enter Password: ");
                var validationResult = _authValidator.ValidatePassword(password);
                if (validationResult.IsValid)
                {
                    return password;
                }

                ConsoleHelper.ShowError(validationResult.ErrorMessage);
                ConsoleHelper.Pause();
            }
        }

        private string GetValidLoginEmail()
        {
            while (true)
            {
                string email = InputHelper.GetRequiredStringInput("Enter Email: ");
                var validationResult = _authValidator.ValidateEmail(email);
                if (validationResult.IsValid)
                {
                    return email;
                }

                ConsoleHelper.ShowError(validationResult.ErrorMessage);
                ConsoleHelper.Pause();
            }
        }

        private string GetValidLoginPassword()
        {
            while (true)
            {
                string password = InputHelper.GetRequiredStringInput("Enter Password: ");
                if (!string.IsNullOrWhiteSpace(password))
                {
                    return password;
                }

                ConsoleHelper.ShowError("Password cannot be empty.");
                ConsoleHelper.Pause();
            }
        }
    }
}