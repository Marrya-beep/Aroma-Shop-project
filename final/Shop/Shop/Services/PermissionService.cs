using Shop.Data;
using System.Linq;

namespace Shop.Services
{
    public class PermissionService
    {
        private readonly ShopDbContext _context;

        public PermissionService(ShopDbContext context)
        {
            _context = context;
        }

        public bool UserHasPermission(int userId, string permissionName)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            // Admin همه چی داره
            if (user.FKRole == 1) return true;

            // چک کردن پرمیشن با جدول RolePermissions و Permissions
            return (from rp in _context.RolePermissions
                    join p in _context.Permissions
                        on rp.FkPermission equals p.Id
                    where rp.FkRole == user.FKRole
                          && p.Name == permissionName
                    select rp).Any();
        }
    }
}

