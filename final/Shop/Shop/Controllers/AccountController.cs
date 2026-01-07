using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopDbContext _context;

        public AccountController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // پاک کردن همه Sessionها
            return RedirectToAction("Login"); // برگردوندن به صفحه لاگین
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == userName
                                  && u.Password == password
                                  && u.IsActive);

            if (user == null)
            {
                ViewBag.Error = "نام کاربری یا رمز اشتباه است";
                return View();
            }

            // ✅ ذخیره Session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetInt32("Role", user.FKRole);
            HttpContext.Session.SetString("UserName", user.UserName);

            // Redirect بر اساس نقش
            if (user.FKRole == 1) // Admin
                return RedirectToAction("NewItem", "AddItem");
            else // User عادی
                return RedirectToAction("Index", "Home");
        }
    }
}
