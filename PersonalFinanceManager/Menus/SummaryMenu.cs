using PersonalFinanceManager.Models;
using PersonalFinanceManager.Services;

namespace PersonalFinanceManager.Menus
{
    public class SummaryMenu
    {
        private readonly SummaryService _summaryService;

        public SummaryMenu(
            SummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public void Show(User loggedInUser)
        {
            _summaryService.ShowSummary(loggedInUser);
        }
    }
}