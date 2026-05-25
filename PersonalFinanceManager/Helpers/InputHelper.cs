namespace PersonalFinanceManager.Helpers
{
    public static class InputHelper
    {
        // ================= INTEGER INPUT =================

        public static int GetValidIntInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = (Console.ReadLine() ?? "").Trim();

                bool isValid = int.TryParse(input, out int result);
                if (isValid)
                {
                    return result;
                }

                ShowError("Please enter a valid number.");
            }
        }

        // ================= DECIMAL INPUT =================

        public static decimal GetValidDecimalInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = (Console.ReadLine() ?? "").Trim();

                bool isValid = decimal.TryParse(input, out decimal result);
                if (isValid)
                {
                    return result;
                }

                ShowError("Please enter a valid amount.");
            }
        }

        // ================= DATE INPUT =================

        public static DateTime GetValidDateInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = (Console.ReadLine() ?? "").Trim();

                bool isValid = DateTime.TryParse(input, out DateTime result);
                if (isValid)
                {
                    return result;
                }

                ShowError("Please enter a valid date.");
            }
        }

        // ================= STRING INPUT =================

        public static string GetRequiredStringInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = (Console.ReadLine() ?? "").Trim();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                ShowError("Input cannot be empty.");
            }
        }

        // ================= ERROR =================

        private static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
        }
    }
}