using PersonalFinanceManager.Models;

namespace PersonalFinanceManager.Repositories.Interfaces
{
    public interface IIncomeRepository
    {
        void Add(Income income);

        List<Income> GetAllByUser(int userId);

        List<Income> GetByCategory(int userId, int categoryId);

        List<Income> GetByDate(int userId, DateTime date);

        Income? GetById(int id);

        void Delete(Income income);

        void Save();
    }
}