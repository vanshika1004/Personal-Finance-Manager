using PersonalFinanceManager.Helpers;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Repositories;
using PersonalFinanceManager.Repositories.Interfaces;
using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Services
{
    public class BudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly BudgetValidator _budgetValidator;

        public BudgetService(IBudgetRepository budgetRepository, ICategoryRepository categoryRepository, BudgetValidator budgetValidator)
        {
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
            _budgetValidator = budgetValidator;
        }

        public void SetBudget(User loggedInUser)
        {
            ConsoleHelper.ShowTitle("===== SET BUDGET =====\n");

            var categories =
                _categoryRepository.GetAll()
                .Where(x => x.Type.ToString() == "Expense")
                .ToList();

            if (!categories.Any())
            {
                ConsoleHelper.ShowError("No Expense Categories Found.");
                ConsoleHelper.Pause();
                return;
            }

            ShowBudgetCategories(categories);
            Category selectedCategory = GetValidBudgetCategory(categories);
            decimal limitAmount = GetValidBudgetAmount();
            int month = GetValidBudgetMonth();
            int year = GetValidBudgetYear();

            Budget budget = new Budget
            {
                LimitAmount = limitAmount,
                Month = month,
                Year = year,
                UserId = loggedInUser.Id,
                CategoryId = selectedCategory.Id
            };

            _budgetRepository.Add(budget);
            _budgetRepository.Save();

            ConsoleHelper.ShowSuccess("\nBudget Added Successfully.");
            ConsoleHelper.Pause();
        }

        public void ViewBudgets(User loggedInUser)
        {

            var budgets = _budgetRepository.GetAllByUser(loggedInUser.Id);
            ConsoleHelper.ShowTitle("=============== ALL BUDGETS ===============\n");

            if (!budgets.Any())
            {
                ConsoleHelper.ShowError("No Budgets Found.");
                ConsoleHelper.Pause();
                return;

            }

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(
                "--------------------------------------------------------------------------------");

            Console.WriteLine(
                $"| {"Id",-5} | {"Category",-15} | {"Budget",-10} | {"Month",-10} | {"Year",-10} |");

            Console.WriteLine(
                "--------------------------------------------------------------------------------");

            foreach (var budget in budgets)
            {
                Console.WriteLine(
                    $"| {budget.Id,-5} | {budget.Category.Name,-15} | {budget.LimitAmount,-10} | {budget.Month,-10} | {budget.Year,-10} |");
            }

            Console.WriteLine(
                "--------------------------------------------------------------------------------");

            Console.ResetColor();

            Console.ReadKey();
        }

        public void TrackBudget(User loggedInUser)
        {
            Console.Clear();

            var budgets = _budgetRepository.GetAllByUser(loggedInUser.Id);

            if (!budgets.Any())
            {
                ConsoleHelper.ShowError("No Budgets Found.");
                ConsoleHelper.Pause();
                return;
            }

            foreach (var budget in budgets)
            {
                decimal totalExpense = _budgetRepository.GetTotalExpenseByCategoryAndMonth(
                        loggedInUser.Id, budget.CategoryId, budget.Month, budget.Year);

                decimal remaining = budget.LimitAmount - totalExpense;

                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("\n================================");

                Console.ResetColor();

                Console.WriteLine($"Category : {budget.Category.Name}");

                Console.WriteLine($"Budget   : {budget.LimitAmount}");

                Console.WriteLine($"Spent    : {totalExpense}");

                Console.WriteLine($"Remaining: {remaining}");

                if (remaining < 0)
                {
                    ConsoleHelper.ShowError("Budget Exceeded!");
                }
            }

            Console.ReadKey();
        }

        public void DeleteBudget(User loggedInUser)
        {
            Console.Clear();

            ViewBudgets(loggedInUser);

            int budgetId = InputHelper.GetValidIntInput("\nEnter Budget Id To Delete: ");
            var budget = _budgetRepository.GetById(budgetId);
            if (budget == null)
            {
                ConsoleHelper.ShowError("Budget Not Found.");
                ConsoleHelper.Pause();
                return;
            }

            if (budget.UserId != loggedInUser.Id)
            {
                ConsoleHelper.ShowError("Unauthorized Action.");
                ConsoleHelper.Pause();
                return;
            }

            _budgetRepository.Delete(budget);
            _budgetRepository.Save();

            ConsoleHelper.ShowSuccess("\nBudget Deleted Successfully.");
            Console.ReadKey();
        }

        private void ShowBudgetCategories(List<Category> categories)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"| {"Id",-5} | {"Category",-20} |");
            Console.WriteLine("------------------------------------------------");

            foreach (var category in categories)
            {
                Console.WriteLine($"| {category.Id,-5} | {category.Name,-20} |");
            }

            Console.WriteLine("------------------------------------------------");
            Console.ResetColor();
        }

        private Category GetValidBudgetCategory(List<Category> categories)
        {
            while (true)
            {
                int categoryId = InputHelper.GetValidIntInput("\nEnter Category Id: ");
                var selectedCategory = categories.FirstOrDefault(x => x.Id == categoryId);

                var validationResult = _budgetValidator.ValidateCategory(selectedCategory);

                if (validationResult.IsValid)
                {
                    return selectedCategory!;
                }

                ConsoleHelper.ShowError(validationResult.ErrorMessage);
                ConsoleHelper.Pause();
            }
        }

        private decimal GetValidBudgetAmount()
        {
            while (true)
            {
                decimal amount = InputHelper.GetValidDecimalInput("\nEnter Budget Amount: ");
                var validationResult = _budgetValidator.ValidateLimitAmount(amount);

                if (validationResult.IsValid)
                {
                    return amount;
                }

                ConsoleHelper.ShowError(validationResult.ErrorMessage);
                ConsoleHelper.Pause();
            }
        }

        private int GetValidBudgetMonth()
        {
            while (true)
            {
                int month = InputHelper.GetValidIntInput("\nEnter Month (1-12): ");
                var validationResult = _budgetValidator.ValidateMonth(month);
                if (validationResult.IsValid)
                {
                    return month;
                }

                ConsoleHelper.ShowError(validationResult.ErrorMessage);
                ConsoleHelper.Pause();
            }
        }

        private int GetValidBudgetYear()
        {
            while (true)
            {
                int year = InputHelper.GetValidIntInput("\nEnter Year: ");
                var validationResult = _budgetValidator.ValidateYear(year);

                if (validationResult.IsValid)
                {
                    return year;
                }

                ConsoleHelper.ShowError(validationResult.ErrorMessage);
                ConsoleHelper.Pause();
            }
        }


    }
}