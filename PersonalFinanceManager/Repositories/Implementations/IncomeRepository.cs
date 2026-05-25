using Microsoft.EntityFrameworkCore;
using PersonalFinanceManager.Data;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Repositories.Interfaces;

namespace PersonalFinanceManager.Repositories.Implementations
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly AppDbContext _context;

        public IncomeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Income income)
        {
            _context.Incomes.Add(income);
        }

        public List<Income> GetAllByUser(int userId)
        {
            return _context.Incomes
                .Include(x => x.Category)
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public List<Income> GetByCategory(int userId, int categoryId)
        {
            return _context.Incomes
                .Include(x => x.Category)
                .Where(x =>
                    x.UserId == userId
                    && x.CategoryId == categoryId)
                .ToList();
        }

        public List<Income> GetByDate(int userId, DateTime date)
        {
            return _context.Incomes
                .Include(x => x.Category)
                .Where(x =>
                    x.UserId == userId
                    && x.Date.Date == date.Date)
                .ToList();
        }

        public Income? GetById(int id)
        {
            return _context.Incomes
                .FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Income income)
        {
            _context.Incomes.Remove(income);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}