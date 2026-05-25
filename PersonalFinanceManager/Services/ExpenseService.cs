using PersonalFinanceManager.Helpers;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Repositories;
using PersonalFinanceManager.Repositories.Interfaces;
using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Services
{
    public class ExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ExpenseValidator _expenseValidator;

        public ExpenseService(IExpenseRepository expenseRepository,
            ICategoryRepository categoryRepository, ExpenseValidator expenseValidator)
        {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            _expenseValidator = expenseValidator;
        }

        // ================= ADD EXPENSE =================

        public void AddExpense(User loggedInUser)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== ADD EXPENSE =====\n");
            Console.ResetColor();

            var categories = _categoryRepository.GetAll()
                .Where(x => x.Type.ToString() == "Expense")
                .ToList();

            if (!categories.Any())
            {
                ShowError("No Expense Categories Found.");
                return;
            }

            ShowExpenseCategories(categories);

            Category selectedCategory = GetValidExpenseCategory(categories);
            decimal amount = GetValidExpenseAmount();
            string description = GetValidExpenseDescription();

            Expense expense = new Expense
            {
                Amount = amount,
                Description = description,
                Date = DateTime.Now,
                UserId = loggedInUser.Id,
                CategoryId = selectedCategory.Id
            };

            _expenseRepository.Add(expense);
            _expenseRepository.Save();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nExpense Added Successfully.");
            Console.ResetColor();
            Console.ReadKey();
        }

        // ================= VIEW EXPENSES =================

        public void ViewExpenses(User loggedInUser)
        {
            Console.Clear();

            var expenses = _expenseRepository.GetAllByUser(loggedInUser.Id);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============= ALL EXPENSES =============\n");
            Console.ResetColor();
            if (!expenses.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No Expenses Found.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("----------------------------------------------------------------------------");

            Console.WriteLine($"| {"Id",-5} | {"Category",-15} | {"Amount",-10} | {"Date",-20} | {"Description",-15} |");

            Console.WriteLine("----------------------------------------------------------------------------");

            foreach (var expense in expenses)
            {
                Console.WriteLine(
                    $"| {expense.Id,-5} | {expense.Category.Name,-15} | {expense.Amount,-10} | {expense.Date,-20} | {expense.Description,-15} |");
            }

            Console.WriteLine(
                "----------------------------------------------------------------------------");

            Console.ResetColor();

            Console.ReadKey();
        }

        // ================= FILTER BY CATEGORY =================

        public void FilterByCategory(User loggedInUser)
        {
            Console.Clear();

            var categories =
                _categoryRepository.GetAll()
                .Where(x => x.Type.ToString() == "Expense")
                .ToList();

            if (!categories.Any())
            {
                Console.WriteLine("No Categories Found.");
                Console.ReadKey();
                return;
            }

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id}. {category.Name}");
            }

            Console.Write("\nEnter Category Id: ");

            int categoryId = InputHelper.GetValidIntInput("\nSelect Option: ");
            var expenses = _expenseRepository.GetByCategory(loggedInUser.Id, categoryId);
            if (!expenses.Any())
            {
                Console.WriteLine("\nNo Expenses Found.");
                Console.ReadKey();
                return;
            }

            foreach (var expense in expenses)
            {
                Console.WriteLine($"{expense.Category.Name} | {expense.Amount} | {expense.Description}");
            }

            Console.ReadKey();
        }

        // ================= FILTER BY DATE =================

        public void FilterByDate(User loggedInUser)
        {
            Console.Clear();
            Console.Write("Enter Date (yyyy-mm-dd): ");
            DateTime date = InputHelper.GetValidDateInput("Enter Date (yyyy-mm-dd): ");

            var expenses = _expenseRepository.GetByDate(loggedInUser.Id, date);

            if (!expenses.Any())
            {
                Console.WriteLine("\nNo Expenses Found.");
                Console.ReadKey();
                return;
            }

            foreach (var expense in expenses)
            {
                Console.WriteLine($"{expense.Category.Name} | {expense.Amount} | {expense.Description}");
            }

            Console.ReadKey();
        }

        // ================= DELETE EXPENSE =================

        public void DeleteExpense(User loggedInUser)
        {
            Console.Clear();

            ViewExpenses(loggedInUser);
            Console.Write("\nEnter Expense Id To Delete: ");

            int expenseId = InputHelper.GetValidIntInput("\nEnter Expense Id To Delete: ");
            var expense = _expenseRepository.GetById(expenseId);
            if (expense == null)
            {
                Console.WriteLine("\nExpense Not Found.");
                Console.ReadKey();
                return;
            }

            // SECURITY CHECK

            if (expense.UserId != loggedInUser.Id)
            {
                Console.WriteLine("\nUnauthorized Action.");
                Console.ReadKey();
                return;
            }

            _expenseRepository.Delete(expense);

            _expenseRepository.Save();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nExpense Deleted Successfully.");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void ShowExpenseCategories(List<Category> categories)
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

        private Category GetValidExpenseCategory(List<Category> categories)
        {
            while (true)
            {
                int categoryId = InputHelper.GetValidIntInput("\nEnter Category Id: ");
                var selectedCategory = categories.FirstOrDefault(x => x.Id == categoryId);
                var validationResult = _expenseValidator.ValidateCategory(selectedCategory);
                if (validationResult.IsValid)
                {
                    return selectedCategory!;
                }

                ShowError(validationResult.ErrorMessage);
            }
        }

        private decimal GetValidExpenseAmount()
        {
            while (true)
            {
                decimal amount = InputHelper.GetValidDecimalInput("\nEnter Amount: ");
                var validationResult = _expenseValidator.ValidateAmount(amount);
                if (validationResult.IsValid)
                {
                    return amount;
                }

                ShowError(validationResult.ErrorMessage);
            }
        }

        private string GetValidExpenseDescription()
        {
            while (true)
            {
                string description = InputHelper.GetRequiredStringInput("\nEnter Description: ");

                var validationResult = _expenseValidator.ValidateDescription(description);

                if (validationResult.IsValid)
                {
                    return description;
                }

                ShowError(validationResult.ErrorMessage);
            }
        }
        private void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}