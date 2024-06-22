using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallForBids.Models
{
    public class Documents
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        [Required,NotMapped]
        [Display(Name="Image")]
        public IFormFile File { get; set; }
        [Required]
        public int SubmissionId { get; set; }
        [ForeignKey("SubmissionId")]
        public virtual Submissions Submission { get; set; }
    }
}
