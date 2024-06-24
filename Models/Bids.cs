using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallForBids.Models
{
    public class Bids
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime LimitDate { get; set; }
        public string Title { get; set; }
        [StringLength(50)]
        public string? Number { get; set; }
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public string? FiscalYear { get; set; } = "FY24";
        public int? OfficeId { get; set; }
        [ForeignKey("OfficeId")]
        public virtual Offices? Office { get; set; }
        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Projects? Project { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<Submissions>? Submissions { get; set; }
        [NotMapped]
        public string Location { get; set; }
        [NotMapped]
        public string Address { get; set; }
        [NotMapped]
        public string ProjectTitle { get; set; }

    }
}
