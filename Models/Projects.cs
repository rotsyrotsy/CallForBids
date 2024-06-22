using System.ComponentModel.DataAnnotations;

namespace CallForBids.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [StringLength(16, MinimumLength = 16)]
        public string Poet { get; set; }
        public ICollection<Bids>? Bids { get; set; }
    }
}
