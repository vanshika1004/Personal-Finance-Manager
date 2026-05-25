using Microsoft.EntityFrameworkCore;
using PersonalFinanceManager.Data;
using PersonalFinanceManager.Repositories.Interfaces;

namespace PersonalFinanceManager.Repositories.Implementations
{
    public class SummaryRepository : ISummaryRepository
    {
        private readonly AppDbContext _context;

        public SummaryRepository(AppDbContext context)
        {
            _context = context;
        }

        public decimal GetTotalIncome(int userId)
        {
            return _context.Incomes
                .Where(x => x.UserId == userId)
                .Sum(x => (decimal?)x.Amount) ?? 0;
        }

        public decimal GetTotalExpense(int userId)
        {
            return _context.Expenses
                .Where(x => x.UserId == userId)
                .Sum(x => (decimal?)x.Amount) ?? 0;
        }

        public decimal GetMonthlyIncome(int userId, int month, int year)
        {
            return _context.Incomes
                .Where(x =>
                    x.UserId == userId
                    && x.Date.Month == month
                    && x.Date.Year == year)
                .Sum(x => (decimal?)x.Amount) ?? 0;
        }

        public decimal GetMonthlyExpense(int userId, int month, int year)
        {
            return _context.Expenses
                .Where(x =>
                    x.UserId == userId
                    && x.Date.Month == month
                    && x.Date.Year == year)
                .Sum(x => (decimal?)x.Amount) ?? 0;
        }

        public string GetHighestExpenseCategory(int userId)
        {
            var category = _context.Expenses
                .Include(x => x.Category)
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.Category.Name)
                .Select(g => new
                {
                    Category = g.Key,

                    Total = g.Sum(x => x.Amount)
                })
                .OrderByDescending(x => x.Total)
                .FirstOrDefault();

            return category?.Category ?? "N/A";
        }

        public string GetHighestIncomeCategory(int userId)
        {
            var category = _context.Incomes
                .Include(x => x.Category)
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.Category.Name)
                .Select(g => new
                {
                    Category = g.Key,

                    Total = g.Sum(x => x.Amount)
                })
                .OrderByDescending(x => x.Total)
                .FirstOrDefault();

            return category?.Category ?? "N/A";
        }
    }
}