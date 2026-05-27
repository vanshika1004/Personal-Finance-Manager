using PersonalFinanceManager.Helpers;
using PersonalFinanceManager.Models;

namespace PersonalFinanceManager.Menus
{
    public class DashboardMenu
    {
        private readonly ExpenseMenu _expenseMenu;
        private readonly IncomeMenu _incomeMenu;
        private readonly BudgetMenu _budgetMenu;
        private readonly CategoryMenu _categoryMenu;
        private readonly SummaryMenu _summaryMenu;

        public DashboardMenu(
            ExpenseMenu expenseMenu,
            IncomeMenu incomeMenu,
            BudgetMenu budgetMenu,
            CategoryMenu categoryMenu, SummaryMenu summaryMenu)
        {
            _expenseMenu = expenseMenu;
            _incomeMenu = incomeMenu;
            _budgetMenu = budgetMenu;
            _categoryMenu = categoryMenu;
            _summaryMenu = summaryMenu;
        }
        public void Show(User loggedInUser)
        {
            bool isDashboardRunning = true;

            while (isDashboardRunning)
            {
                Console.Clear();

                Console.WriteLine(
                    $"===== WELCOME {loggedInUser.Username} =====\n");

                Console.WriteLine("1. Manage Expenses");
                Console.WriteLine("2. Manage Income");
                Console.WriteLine("3. Manage Budget");
                Console.WriteLine("4. Manage Categories");
                Console.WriteLine("5. Summary Dashboard");
                Console.WriteLine("6. Logout");

                int choice = InputHelper.GetValidIntInput("\nSelect Option: ");

                switch (choice)
                {
                    case 1:

                        Console.WriteLine("\nExpense Management");
                        _expenseMenu.Show(loggedInUser);
                        Console.ReadKey();
                        break;

                    case 2:

                        Console.WriteLine("\nIncome Management");
                        _incomeMenu.Show(loggedInUser);
                        Console.ReadKey();
                        break;

                    case 3:

                        Console.WriteLine("\nBudget Management");
                        _budgetMenu.Show(loggedInUser);
                        Console.ReadKey();
                        break;

                    case 4:

                        Console.WriteLine("\nCategory Management");
                        _categoryMenu.Show();
                        Console.ReadKey();
                        break;

                    case 5:

                        Console.WriteLine("\nSummary Dashboard");
                        _summaryMenu.Show(loggedInUser);
                        Console.ReadKey();
                        break;

                    case 6:

                        isDashboardRunning = false;
                        Console.WriteLine("\nLogged Out Successfully.");
                        break;

                    default:

                        Console.WriteLine("\nInvalid Choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}