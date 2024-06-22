using System.ComponentModel.DataAnnotations;

namespace CallForBids.Models
{
    public class Offices
    {
        public int Id { get; set; }
        public string Location { get; set; }
        [StringLength(3, MinimumLength = 2)]
        public string? Code { get; set; }
        public string? Address { get; set; }
        public ICollection<Bids>? Bids { get; set; }
    }
}
