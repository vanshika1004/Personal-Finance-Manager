using Microsoft.EntityFrameworkCore;
using PersonalFinanceManager.Data;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Repositories.Interfaces;

namespace PersonalFinanceManager.Repositories.Implementations
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly AppDbContext _context;

        public BudgetRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Budget budget)
        {
            _context.Budgets.Add(budget);
        }

        public List<Budget> GetAllByUser(int userId)
        {
            return _context.Budgets
                .Include(x => x.Category)
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public Budget? GetById(int id)
        {
            return _context.Budgets
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Budget budget)
        {
            _context.Budgets.Remove(budget);
        }

        public decimal GetTotalExpenseByCategoryAndMonth(
            int userId,
            int categoryId,
            int month,
            int year)
        {
            return _context.Expenses
                .Where(x =>
                    x.UserId == userId
                    && x.CategoryId == categoryId
                    && x.Date.Month == month
                    && x.Date.Year == year)
                .Sum(x => (decimal?)x.Amount) ?? 0;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}