using PersonalFinanceManager.Models;
using PersonalFinanceManager.Repositories.Interfaces;

namespace PersonalFinanceManager.Services
{
    public class SummaryService
    {
        private readonly ISummaryRepository _summaryRepository;

        private readonly IBudgetRepository _budgetRepository;

        public SummaryService(ISummaryRepository summaryRepository, IBudgetRepository budgetRepository)
        {
            _summaryRepository = summaryRepository;
            _budgetRepository = budgetRepository;
        }

        public void ShowSummary(User loggedInUser)
        {
            Console.Clear();

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            decimal totalIncome = _summaryRepository.GetTotalIncome(loggedInUser.Id);
            decimal totalExpense = _summaryRepository.GetTotalExpense(loggedInUser.Id);
            decimal balance = totalIncome - totalExpense;
            decimal monthlyIncome = _summaryRepository.GetMonthlyIncome(loggedInUser.Id, currentMonth, currentYear);
            decimal monthlyExpense = _summaryRepository.GetMonthlyExpense(loggedInUser.Id, currentMonth, currentYear);
            string highestExpenseCategory = _summaryRepository.GetHighestExpenseCategory(loggedInUser.Id);
            string highestIncomeCategory = _summaryRepository.GetHighestIncomeCategory(loggedInUser.Id);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== FINANCIAL SUMMARY ==========\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Total Income        : ₹{totalIncome}");
            Console.WriteLine($"Total Expense       : ₹{totalExpense}");
            Console.WriteLine($"Current Balance     : ₹{balance}");
            Console.WriteLine();
            Console.WriteLine($"This Month Income   : ₹{monthlyIncome}");
            Console.WriteLine($"This Month Expense  : ₹{monthlyExpense}");
            Console.WriteLine();
            Console.WriteLine($"Highest Expense Category : {highestExpenseCategory}");
            Console.WriteLine($"Highest Income Category  : {highestIncomeCategory}");

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n========================================");
            Console.ResetColor();

            Console.ReadKey();
        }
    }
}