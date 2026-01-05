using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Data;
using System.Linq;


namespace Shop.Controllers
{
    public class AddItem : Controller
    {
        private readonly ShopDbContext _context;

        public AddItem(ShopDbContext context)
        {
            _context = context;
        }

        public IActionResult NewItem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewItem(string categoryName, ShopItem item)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });


             var category = _context.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                category = new Category { Name = categoryName };
                _context.Categories.Add(category);
                _context.SaveChanges();
            }

            item.IdCategory = category.Id;
            _context.ShopItems.Add(item);
            _context.SaveChanges();

            return Json(new { success = true, message = "محصول با موفقیت ذخیره شد!", item });
        }
        }

    
}
