using PersonalFinanceManager.Data;
using PersonalFinanceManager.Enums;
using PersonalFinanceManager.Models;

namespace PersonalFinanceManager.Repositories.Interfaces
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category? GetById(int id)
        {
            return _context.Categories
                .FirstOrDefault(x => x.Id == id);
        }

        public bool Exists(
            string name,
            CategoryType type)
        {
            return _context.Categories.Any(x =>
                x.Name.ToLower() == name.ToLower()
                && x.Type == type);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}