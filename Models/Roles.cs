using System.ComponentModel.DataAnnotations;

namespace CallForBids.Models
{
    public class Roles
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Users>? Users { get; set; }

    }
}
