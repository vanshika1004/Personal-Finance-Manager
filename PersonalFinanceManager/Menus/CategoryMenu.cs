using PersonalFinanceManager.Services;

namespace PersonalFinanceManager.Menus
{
    public class CategoryMenu
    {
        private readonly CategoryService _categoryService;

        public CategoryMenu(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void Show()
        {
            bool isCategoryMenuRunning = true;

            while (isCategoryMenuRunning)
            {
                Console.Clear();

                Console.WriteLine("===== CATEGORY MANAGEMENT =====\n");

                Console.WriteLine("1. Add Category");
                Console.WriteLine("2. View Categories");
                Console.WriteLine("3. Delete Category");
                Console.WriteLine("4. Back");

                Console.Write("\nSelect Option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:

                        _categoryService.AddCategory();
                        break;

                    case 2:

                        _categoryService.ViewCategories();
                        break;

                    case 3:

                        _categoryService.DeleteCategory();
                        break;

                    case 4:

                        isCategoryMenuRunning = false;
                        break;

                    default:

                        Console.WriteLine("\nInvalid Choice.");

                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}