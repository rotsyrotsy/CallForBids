using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallForBids.Models
{
    public class Users
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Suppliers? Suppliers { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Roles Role { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public ICollection<Submissions>? Submissions { get; set; }

    }
}
