using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System.Linq;
using System.Collections.Generic;

namespace Shop.Controllers
{
    public class SearchingProduct : Controller
    {
        private readonly ShopDbContext _context;

        public SearchingProduct(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View(new List<ShopItem>());
        }

        [HttpPost]
        public IActionResult Search(string query)
        {
            var results = new List<ShopItem>();

            if (!string.IsNullOrWhiteSpace(query))
            {
                results = _context.ShopItems
                    .Where(x => x.Name.Contains(query))
                    .ToList();
            }

            ViewBag.Query = query;
            return View(results);
        }
    }
}
