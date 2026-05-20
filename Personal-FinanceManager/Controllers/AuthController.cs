using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManager.Data;
using PersonalFinanceManager.Dto;
using PersonalFinanceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceManager.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // ================= REGISTER PAGE =================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                ViewBag.ErrorMessage = "Passwords do not match.";
                return View(dto);
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "User already exists with this email.";
                return View(dto);
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            User user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Registration successful. Please login.";

            return RedirectToAction("Login");
        }

        // ================= LOGIN PAGE =================
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View();
        }

        // ================= LOGIN USER =================

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
            {
                ViewBag.ErrorMessage = "User does not exist.";
                return View(dto);
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                ViewBag.ErrorMessage = "Incorrect password.";
                return View(dto);
            }

            // SESSION STORAGE
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Dashboard");
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
