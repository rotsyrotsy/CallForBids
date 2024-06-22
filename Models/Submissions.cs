using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallForBids.Models
{
    public class Submissions
    {
        public int Id { get; set; }
        public int BidId { get; set; }
        [ForeignKey("BidId")]
        public virtual Bids Bid { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int? State { get; set; } = null;
        [DataType(DataType.Text)]
        public string? Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CancellationDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ValidationDate { get; set; }
        public ICollection<Documents>? Documents { get; set; }
    }
}
