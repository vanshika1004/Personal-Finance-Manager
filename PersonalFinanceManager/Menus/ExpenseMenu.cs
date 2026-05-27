using PersonalFinanceManager.Helpers;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Services;

namespace PersonalFinanceManager.Menus
{
    public class ExpenseMenu
    {
        private readonly ExpenseService _expenseService;

        public ExpenseMenu(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public void Show(User loggedInUser)
        {
            bool isExpenseMenuRunning = true;

            while (isExpenseMenuRunning)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== EXPENSE MANAGEMENT =====\n");
                Console.ResetColor();

                Console.WriteLine("1. Add Expense");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. Filter By Category");
                Console.WriteLine("4. Filter By Date");
                Console.WriteLine("5. Delete Expense");
                Console.WriteLine("6. Back");

                int choice = InputHelper.GetValidIntInput("\nSelect Option: ");
                switch (choice)
                {
                    case 1:

                        _expenseService.AddExpense(loggedInUser);
                        break;

                    case 2:

                        _expenseService.ViewExpenses(loggedInUser);
                        break;

                    case 3:

                        _expenseService.FilterByCategory(loggedInUser);
                        break;

                    case 4:

                        _expenseService.FilterByDate(loggedInUser);
                        break;

                    case 5:

                        _expenseService.DeleteExpense(loggedInUser);
                        break;

                    case 6:

                        isExpenseMenuRunning = false;
                        break;

                    default:

                        ConsoleHelper.ShowError("\nInvalid Choice.");
                        ConsoleHelper.Pause();
                        break;
                }
            }
        }
    }
}