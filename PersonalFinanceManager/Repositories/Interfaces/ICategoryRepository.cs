using PersonalFinanceManager.Enums;
using PersonalFinanceManager.Models;

namespace PersonalFinanceManager.Repositories
{
    public interface ICategoryRepository
    {
        void Add(Category category);

        List<Category> GetAll();

        Category? GetById(int id);

        bool Exists(string name, CategoryType type);

        void Delete(Category category);

        void Save();
    }
}