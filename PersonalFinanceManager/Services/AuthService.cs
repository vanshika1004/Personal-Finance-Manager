using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceManager.Data;
using PersonalFinanceManager.Models;

namespace PersonalFinanceManager.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        // ================= REGISTER =================

        public void Register()
        {
            Console.Clear();

            Console.WriteLine("===== REGISTER =====");

            Console.Write("Enter Username: ");
            string username = (Console.ReadLine() ?? "").Trim();

            Console.Write("Enter Email: ");
            string email = (Console.ReadLine() ?? "").Trim();

            Console.Write("Enter Password: ");
            string password = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine(password);

            var existingUser = _context.Users
                .FirstOrDefault(x => x.Email == email);

            if (existingUser != null)
            {
                Console.WriteLine("\nUser already exists.");
                return;
            }

            string hashedPassword =
                BCrypt.Net.BCrypt.HashPassword(password);

            User user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);

            _context.SaveChanges();

            Console.WriteLine("\nRegistration Successful.");
        }

        // ================= LOGIN =================

        public User? Login()
        {
            Console.Clear();

            Console.WriteLine("===== LOGIN =====");

            Console.Write("Enter Email: ");
            string email = (Console.ReadLine() ?? "").Trim();

            Console.Write("Enter Password: ");
            string password = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine(password);

            var user = _context.Users
                .FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                Console.WriteLine("\nUser does not exist.");
                return null;
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    password,
                    user.PasswordHash);

            if (!isPasswordValid)
            {
                Console.WriteLine("\nIncorrect Password.");
                return null;
            }

            Console.WriteLine($"\nWelcome {user.Username}");

            return user;
        }
    }
}