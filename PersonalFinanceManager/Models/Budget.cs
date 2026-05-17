namespace PersonalFinanceManager.Models
{
    public class Budget
    {
        public int Id { get; set; }

        public decimal LimitAmount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        // Category Budget
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // User Relationship
        public int UserId { get; set; }
        public User User { get; set; }
    }
}