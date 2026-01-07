using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class RolePermission
    {
        public int FkRole { get; set; }
        public int FkPermission { get; set; }

        [ForeignKey("FKRole")]
        public Role Role { get; set; }

        [ForeignKey("FKPermission")]
        public Permission Permission { get; set; }
    }
}
