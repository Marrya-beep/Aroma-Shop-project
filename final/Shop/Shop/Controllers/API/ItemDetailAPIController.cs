using Microsoft.AspNetCore.Mvc;
using Shop.Data;

namespace Shop.Controllers.API
{
    [ApiController]
    [Route("api/item-detail")]
    public class ItemDetailAPIController : Controller
    {
        private readonly ShopDbContext _context;

        public ItemDetailAPIController(ShopDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult GetItemDetail(int id)
        {
            var product = _context.ShopItems
                .FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            var category = _context.Categories
                .FirstOrDefault(c => c.Id == product.IdCategory);

            return Ok(new
            {
                product,
                categoryName = category?.Name ?? "نامشخص"
            });
        }
    }
}
