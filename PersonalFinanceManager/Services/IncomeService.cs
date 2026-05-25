using PersonalFinanceManager.Helpers;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Repositories;
using PersonalFinanceManager.Repositories.Interfaces;
using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Services
{
    public class IncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IncomeValidator _incomeValidator;

        public IncomeService(IIncomeRepository incomeRepository, ICategoryRepository categoryRepository,
            IncomeValidator incomeValidator)
        {
            _incomeRepository = incomeRepository;
            _categoryRepository = categoryRepository;
            _incomeValidator = incomeValidator;
        }

        // ================= ADD INCOME =================

        public void AddIncome(User loggedInUser)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== ADD INCOME =====\n");
            Console.ResetColor();

            var categories =
                _categoryRepository.GetAll()
                .Where(x => x.Type.ToString() == "Income")
                .ToList();

            if (!categories.Any())
            {
                ShowError("No Income Categories Found.");
                return;
            }
            ShowIncomeCategories(categories);
            Category selectedCategory = GetValidIncomeCategory(categories);
            decimal amount = GetValidIncomeAmount();
            string source = GetValidIncomeSource();

            Income income = new Income
            {
                Amount = amount,
                Source = source,
                Date = DateTime.Now,
                UserId = loggedInUser.Id,
                CategoryId = selectedCategory.Id
            };

            _incomeRepository.Add(income);
            _incomeRepository.Save();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nIncome Added Successfully.");
            Console.ResetColor();
            Console.ReadKey();
        }

        // ================= VIEW INCOME =================

        public void ViewIncome(User loggedInUser)
        {
            Console.Clear();

            var incomes = _incomeRepository.GetAllByUser(loggedInUser.Id);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=============== ALL INCOME ===============\n");
            Console.ResetColor();

            if (!incomes.Any())
            {
                ShowError("No Income Found.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(
                "--------------------------------------------------------------------------------");

            Console.WriteLine(
                $"| {"Id",-5} | {"Category",-15} | {"Amount",-10} | {"Date",-20} | {"Source",-15} |");

            Console.WriteLine(
                "--------------------------------------------------------------------------------");

            foreach (var income in incomes)
            {
                Console.WriteLine(
                    $"| {income.Id,-5} | {income.Category.Name,-15} | {income.Amount,-10} | {income.Date,-20} | {income.Source,-15} |");
            }

            Console.WriteLine(
                "--------------------------------------------------------------------------------");

            Console.ResetColor();
            Console.ReadKey();
        }

        // ================= FILTER BY CATEGORY =================

        public void FilterByCategory(User loggedInUser)
        {
            Console.Clear();

            var categories =
                _categoryRepository.GetAll()
                .Where(x => x.Type.ToString() == "Income")
                .ToList();

            if (!categories.Any())
            {
                ShowError("No Income Categories Found.");
                return;
            }
            ShowIncomeCategories(categories);
            Category selectedCategory = GetValidIncomeCategory(categories);

            var incomes = _incomeRepository.GetByCategory(loggedInUser.Id, selectedCategory.Id);

            if (!incomes.Any())
            {
                ShowError("No Income Found.");
                return;
            }

            foreach (var income in incomes)
            {
                Console.WriteLine(
                    $"{income.Category.Name} | {income.Amount} | {income.Source}");
            }

            Console.ReadKey();
        }

        // ================= FILTER BY DATE =================

        public void FilterByDate(User loggedInUser)
        {
            Console.Clear();

            DateTime date = InputHelper.GetValidDateInput("Enter Date (yyyy-mm-dd): ");

            var incomes = _incomeRepository.GetByDate(loggedInUser.Id, date);
            if (!incomes.Any())
            {
                ShowError("No Income Found.");
                return;
            }

            foreach (var income in incomes)
            {
                Console.WriteLine(
                    $"{income.Category.Name} | {income.Amount} | {income.Source}");
            }

            Console.ReadKey();
        }

        // ================= DELETE INCOME =================

        public void DeleteIncome(User loggedInUser)
        {
            Console.Clear();

            ViewIncome(loggedInUser);

            int incomeId = InputHelper.GetValidIntInput("\nEnter Income Id To Delete: ");

            var income = _incomeRepository.GetById(incomeId);
            if (income == null)
            {
                ShowError("Income Not Found.");
                return;
            }

            if (income.UserId != loggedInUser.Id)
            {
                ShowError("Unauthorized Action.");
                return;
            }

            _incomeRepository.Delete(income);
            _incomeRepository.Save();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nIncome Deleted Successfully.");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void ShowIncomeCategories(List<Category> categories)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"| {"Id",-5} | {"Category",-20} |");
            Console.WriteLine("------------------------------------------------");

            foreach (var category in categories)
            {
                Console.WriteLine(
                    $"| {category.Id,-5} | {category.Name,-20} |");
            }

            Console.WriteLine("------------------------------------------------");
            Console.ResetColor();
        }

        private Category GetValidIncomeCategory(List<Category> categories)
        {
            while (true)
            {
                int categoryId = InputHelper.GetValidIntInput("\nEnter Category Id: ");

                var selectedCategory =
                    categories.FirstOrDefault(
                        x => x.Id == categoryId);

                var validationResult =
                    _incomeValidator.ValidateCategory(
                        selectedCategory);

                if (validationResult.IsValid)
                {
                    return selectedCategory!;
                }

                ShowError(
                    validationResult.ErrorMessage);
            }
        }

        private decimal GetValidIncomeAmount()
        {
            while (true)
            {
                decimal amount =
                    InputHelper.GetValidDecimalInput(
                        "\nEnter Amount: ");

                var validationResult =
                    _incomeValidator.ValidateAmount(
                        amount);

                if (validationResult.IsValid)
                {
                    return amount;
                }

                ShowError(
                    validationResult.ErrorMessage);
            }
        }

        private string GetValidIncomeSource()
        {
            while (true)
            {
                string source =
                    InputHelper.GetRequiredStringInput(
                        "\nEnter Source: ");

                var validationResult =
                    _incomeValidator.ValidateSource(
                        source);

                if (validationResult.IsValid)
                {
                    return source;
                }

                ShowError(
                    validationResult.ErrorMessage);
            }
        }

        private void ShowError(string message)
        {
            Console.ForegroundColor =
                ConsoleColor.Red;

            Console.WriteLine($"\n{message}");

            Console.ResetColor();

            Console.ReadKey();
        }

    }
}