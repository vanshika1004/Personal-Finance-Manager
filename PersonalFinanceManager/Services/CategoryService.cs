using PersonalFinanceManager.Enums;
using PersonalFinanceManager.Helpers;
using PersonalFinanceManager.Models;
using PersonalFinanceManager.Repositories.Interfaces;
using PersonalFinanceManager.Validators;

namespace PersonalFinanceManager.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryValidator _categoryValidator;

        public CategoryService(ICategoryRepository categoryRepository, CategoryValidator categoryValidator)
        {
            _categoryRepository = categoryRepository;
            _categoryValidator = categoryValidator;
        }

        public void AddCategory()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== ADD CATEGORY =====\n");
            Console.ResetColor();

            string name = GetValidCategoryName();
            CategoryType type = GetValidCategoryType();

            bool exists = _categoryRepository.Exists(name, type);

            if (exists)
            {
                ShowError("Category already exists.");
                return;
            }

            Category category = new Category
            {
                Name = name,
                Type = type
            };

            _categoryRepository.Add(category);
            _categoryRepository.Save();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCategory Added Successfully.");
            Console.ResetColor();
            Console.ReadKey();
        }

        public void ViewCategories()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine( "=========== ALL CATEGORIES ===========\n");
            Console.ResetColor();

            var categories = _categoryRepository.GetAll();

            if (!categories.Any())
            {
                ShowError("No Categories Found.");
                return;
            }

            // TABLE HEADER
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"| {"Id",-5} | {"Category Name",-20} | {"Type",-10} |");
            Console.WriteLine("------------------------------------------------");
            Console.ResetColor();
            // TABLE ROWS

            foreach (var category in categories)
            {
                Console.WriteLine( $"| {category.Id,-5} | {category.Name,-20} | {category.Type,-10} |");
            }

            Console.WriteLine( "------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        public void DeleteCategory()
        {
            Console.Clear();
            ViewCategories();

            Console.Write("\nEnter Category Id: ");

            int categoryId = InputHelper.GetValidIntInput("\nEnter Category Id: ");
            var category = _categoryRepository.GetById(categoryId);
            if (category == null)
            {
                ShowError("Category Not Found.");
                return;
            }

            _categoryRepository.Delete(category);
            _categoryRepository.Save();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCategory Deleted Successfully.");
            Console.ResetColor();
            Console.ReadKey();
        }

        private string GetValidCategoryName()
        {
            while (true)
            {
                string name = InputHelper.GetRequiredStringInput("Enter Category Name: ");
                var validationResult = _categoryValidator.ValidateCategoryName(name);
                if (validationResult.IsValid)
                {
                    return name;
                }

                ShowError(validationResult.ErrorMessage);
            }
        }

        private CategoryType GetValidCategoryType()
        {
            while (true)
            {
                Console.WriteLine("\n1. Expense");
                Console.WriteLine("2. Income");

                int choice = InputHelper.GetValidIntInput("\nSelect Type: ");
                var validationResult = _categoryValidator.ValidateCategoryType(choice);
                if (validationResult.IsValid)
                {
                    return choice == 1 ? CategoryType.Expense : CategoryType.Income;
                }

                ShowError(validationResult.ErrorMessage);
            }
        }

        private void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}