using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceManager.Data;
using PersonalFinanceManager.Helpers;
using PersonalFinanceManager.Menus;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Repositories;
using PersonalFinanceManager.Repositories.Implementations;
using PersonalFinanceManager.Repositories.Interfaces;
using PersonalFinanceManager.Services;
using PersonalFinanceManager.Validators;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();

// ================= DATABASE =================

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection")));

// ================= MENUS =================

services.AddScoped<DashboardMenu>();
services.AddScoped<ExpenseMenu>();
services.AddScoped<IncomeMenu>();
services.AddScoped<BudgetMenu>();
services.AddScoped<CategoryMenu>();
services.AddScoped<SummaryMenu>();

// ================= SERVICES =================

services.AddScoped<AuthService>();
services.AddScoped<CategoryService>();
services.AddScoped<ExpenseService>();
services.AddScoped<IncomeService>();
services.AddScoped<BudgetService>();
services.AddScoped<SummaryService>();


services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<IExpenseRepository, ExpenseRepository>();
services.AddScoped<IIncomeRepository, IncomeRepository>();
services.AddScoped<IBudgetRepository, BudgetRepository>();
services.AddScoped<ISummaryRepository, SummaryRepository>();


services.AddScoped<AuthValidator>();
services.AddScoped<ExpenseValidator>();
services.AddScoped<IncomeValidator>();
services.AddScoped<BudgetValidator>();
services.AddScoped<CategoryValidator>();

var serviceProvider = services.BuildServiceProvider();

// ================= SERVICE RESOLUTION =================

var authService = serviceProvider.GetRequiredService<AuthService>();
var dashboardMenu = serviceProvider.GetRequiredService<DashboardMenu>();
var expenseMenu = serviceProvider.GetRequiredService<ExpenseMenu>();
var incomeMenu = serviceProvider.GetRequiredService<IncomeMenu>();
var budgetMenu = serviceProvider.GetRequiredService<BudgetMenu>();
var categoryMenu = serviceProvider.GetRequiredService<CategoryMenu>();

bool isRunning = true;

// ================= MAIN MENU =================

while (isRunning)
{
    Console.Clear();

    Console.WriteLine("===== Personal Finance Manager =====\n");

    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");
    Console.WriteLine("3. Exit");

    int choice = InputHelper.GetValidIntInput("\nSelect Option: ");

    switch (choice)
    {
        case 1:

            authService.Register();
            break;

        case 2:

            User? loggedInUser = authService.Login();

            if (loggedInUser != null)
            {
                Console.WriteLine("\nLogin Successful.");
                Console.WriteLine("\nPress any key...");
                Console.ReadKey();

                dashboardMenu.Show(loggedInUser);
            }

            break;

        case 3:

            isRunning = false;
            Console.WriteLine("\nApplication Closed.");
            break;

        default:

            Console.WriteLine("\nInvalid Choice.");
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();

            break;
    }
}