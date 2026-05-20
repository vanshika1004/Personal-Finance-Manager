using Microsoft.AspNetCore.Mvc;

namespace PersonalFinanceManager.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
