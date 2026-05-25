namespace PersonalFinanceManager.Validators
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}