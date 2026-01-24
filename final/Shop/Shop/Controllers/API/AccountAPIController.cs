using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using Shop.Models.Dtos;

namespace Shop.Controllers.API
{
    [ApiController]
    [Route("api/user")]
    public class AccountApiController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public AccountApiController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet("get")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Password,
                    u.Email,
                    u.FkRole,
                    u.IsActive,
                    u.CreatedAt
                    
                })
                .ToList();

            return Ok(users);
        }

        [HttpPost("post")]
        public IActionResult CreateAccount([FromBody] CreateAccountDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "نام کاربری و رمز عبور الزامی است"
                });
            }

            if (_context.Users.Any(u => u.UserName == dto.UserName))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "این نام کاربری قبلاً استفاده شده"
                });
            }

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = dto.Password,
                FkRole = 2, 
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                success = true,
                message = "اکانت با موفقیت ساخته شد",
                user = new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.FkRole
                }
            });
        }
    }
}
