using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models; 

namespace Shop.Controllers
{
    public class CreateAccountController : Controller
    {
        private readonly ShopDbContext _context;

        public CreateAccountController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string userName, string email, string password)
        {
            if (_context.Users.Any(u => u.UserName == userName))
            {
                ViewBag.Error = "این نام کاربری قبلاً استفاده شده";
                return View();
            }

            var user = new User
            {
                UserName = userName,
                Email = email,
                Password = password,
                FKRole = 2,          
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            ViewBag.Success = "اکانت شما با موفقیت ایجاد شد!";
            return View();
        }
    }
}
