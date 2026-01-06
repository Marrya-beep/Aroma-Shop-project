using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using Shop.Models.Dtos;


namespace Shop.Controllers.API
{

    [ApiController]
    [Route("api/contact")]
    public class ContactUsAPIController : ControllerBase
    {

        private readonly ShopDbContext _context;

        public ContactUsAPIController(ShopDbContext context)
        {
            _context = context;
        }
        [HttpPost("Send-Message")]
        public IActionResult SendMessage([FromBody] ContactMessageDto dto)
        {
            var message = new ContactMessage
            {
                UserName = dto.UserName,
                Phone = dto.Phone,
                Email = dto.Email,
                Message = dto.Message
            };

            _context.Message.Add(message);
            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "پیام شما با موفقیت ارسال شد!"
            });
        }
    }
}
