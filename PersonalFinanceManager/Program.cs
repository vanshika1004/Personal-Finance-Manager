using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceManager.Data;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Services;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<AuthService>();

var serviceProvider = services.BuildServiceProvider();

var authService =
    serviceProvider.GetRequiredService<AuthService>();

bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("\n===== Personal Finance Manager =====");

    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");
    Console.WriteLine("3. Exit");

    Console.Write("\nSelect Option: ");

    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:

            authService.Register();
            break;

        case 2:

            User loggedInUser =
                authService.Login();

            if (loggedInUser != null)
            {
                Console.WriteLine("\nLogin Successful.");
            }

            break;

        case 3:

            isRunning = false;
            break;

        default:

            Console.WriteLine("\nInvalid Choice.");
            break;
    }
}