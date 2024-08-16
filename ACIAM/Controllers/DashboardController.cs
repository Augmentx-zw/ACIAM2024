using Microsoft.AspNetCore.Mvc;

namespace ACIAM.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
