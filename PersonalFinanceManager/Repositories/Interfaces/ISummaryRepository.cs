namespace PersonalFinanceManager.Repositories.Interfaces
{
    public interface ISummaryRepository
    {
        decimal GetTotalIncome(int userId);

        decimal GetTotalExpense(int userId);

        decimal GetMonthlyIncome(int userId, int month, int year);

        decimal GetMonthlyExpense(int userId, int month, int year);

        string GetHighestExpenseCategory(int userId);

        string GetHighestIncomeCategory(int userId);
    }
}