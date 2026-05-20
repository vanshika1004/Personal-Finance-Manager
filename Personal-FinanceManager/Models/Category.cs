using PersonalFinanceManager.Enums;

namespace PersonalFinanceManager.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Expense or Income
        public CategoryType Type { get; set; }

        // Navigation Properties
        public List<Expense> Expenses { get; set; }

        public List<Income> Incomes { get; set; }

        public List<Budget> Budgets { get; set; }
    }
}