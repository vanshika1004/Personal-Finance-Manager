using PersonalFinanceManager.Helpers;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Services;

namespace PersonalFinanceManager.Menus
{
    public class IncomeMenu
    {
        private readonly IncomeService _incomeService;

        public IncomeMenu(IncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        public void Show(User loggedInUser)
        {
            bool isIncomeMenuRunning = true;

            while (isIncomeMenuRunning)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("===== INCOME MANAGEMENT =====\n");
                Console.ResetColor();
                Console.WriteLine("1. Add Income");
                Console.WriteLine("2. View Income");
                Console.WriteLine("3. Filter By Category");
                Console.WriteLine("4. Filter By Date");
                Console.WriteLine("5. Delete Income");
                Console.WriteLine("6. Back");
                
                int choice = InputHelper.GetValidIntInput("\nSelect Option: ");

                switch (choice)
                {
                    case 1:

                        _incomeService.AddIncome(loggedInUser);
                        break;

                    case 2:

                        _incomeService.ViewIncome(loggedInUser);
                        break;

                    case 3:

                        _incomeService.FilterByCategory(loggedInUser);
                        break;

                    case 4:

                        _incomeService.FilterByDate(loggedInUser);
                        break;

                    case 5:

                        _incomeService.DeleteIncome(loggedInUser);
                        break;

                    case 6:

                        isIncomeMenuRunning = false;
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