using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class Profile : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
