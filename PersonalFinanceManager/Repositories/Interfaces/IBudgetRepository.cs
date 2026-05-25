using PersonalFinanceManager.Models;

namespace PersonalFinanceManager.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        void Add(Budget budget);

        List<Budget> GetAllByUser(int userId);

        Budget? GetById(int id);

        void Delete(Budget budget);

        decimal GetTotalExpenseByCategoryAndMonth(
            int userId,
            int categoryId,
            int month,
            int year);

        void Save();
    }
}