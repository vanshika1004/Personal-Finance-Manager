using Microsoft.EntityFrameworkCore;
using PersonalFinanceManager.Data;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Repositories.Interfaces;

namespace PersonalFinanceManager.Repositories.Implementations
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Expense expense)
        {
            _context.Expenses.Add(expense);
        }

        public List<Expense> GetAllByUser(int userId)
        {
            return _context.Expenses
                .Include(x => x.Category)
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public List<Expense> GetByCategory(
            int userId,
            int categoryId)
        {
            return _context.Expenses
                .Include(x => x.Category)
                .Where(x =>
                    x.UserId == userId
                    && x.CategoryId == categoryId)
                .ToList();
        }

        public List<Expense> GetByDate(int userId, DateTime date)
        {
            return _context.Expenses
                .Include(x => x.Category)
                .Where(x =>
                    x.UserId == userId
                    && x.Date.Date == date.Date)
                .ToList();
        }

        public Expense? GetById(int id)
        {
            return _context.Expenses.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Expense expense)
        {
            _context.Expenses.Remove(expense);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}