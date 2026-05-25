using PersonalFinanceManager.Models;

namespace PersonalFinanceManager.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        void Add(Expense expense);

        List<Expense> GetAllByUser(int userId);

        List<Expense> GetByCategory(int userId, int categoryId);

        List<Expense> GetByDate(int userId, DateTime date);

        Expense? GetById(int id);

        void Delete(Expense expense);

        void Save();
    }
}