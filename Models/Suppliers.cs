using System.ComponentModel.DataAnnotations;

namespace CallForBids.Models
{
    public class Suppliers
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SupplierNumber { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public Users? User { get; set; }
    }
}
