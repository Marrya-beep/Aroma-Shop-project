using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("ContactMessage")]
    public class ContactMessage
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
