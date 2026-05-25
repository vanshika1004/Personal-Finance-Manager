using PersonalFinanceManager.Models;
using PersonalFinanceManager.Services;

namespace PersonalFinanceManager.Menus
{
    public class BudgetMenu
    {
        private readonly BudgetService _budgetService;

        public BudgetMenu(BudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        public void Show(User loggedInUser)
        {
            bool isBudgetMenuRunning = true;

            while (isBudgetMenuRunning)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== BUDGET MANAGEMENT =====\n");
                Console.ResetColor();
                Console.WriteLine("1. Set Budget");
                Console.WriteLine("2. View Budgets");
                Console.WriteLine("3. Track Budget Usage");
                Console.WriteLine("4. Delete Budget");
                Console.WriteLine("5. Back");
                Console.Write("\nSelect Option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:

                        _budgetService.SetBudget(loggedInUser);
                        break;

                    case 2:

                        _budgetService.ViewBudgets(loggedInUser);
                        break;

                    case 3:

                        _budgetService.TrackBudget(loggedInUser);
                        break;

                    case 4:

                        _budgetService.DeleteBudget(loggedInUser);
                        break;

                    case 5:

                        isBudgetMenuRunning = false;
                        break;

                    default:

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid Choice.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}