using Microsoft.AspNetCore.Mvc;

namespace Autonuoma.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
