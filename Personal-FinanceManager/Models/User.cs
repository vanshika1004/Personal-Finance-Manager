namespace PersonalFinanceManager.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public List<Expense> Expenses { get; set; }

        public List<Income> Incomes { get; set; }

        public List<Budget> Budgets { get; set; }
    }
}
