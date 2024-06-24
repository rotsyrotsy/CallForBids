using NuGet.ContentModel;

namespace CallForBids.Models
{
    public class ExportSubmissionsToPdf
    {
        public Users Users { get; set; }
        public List<Submissions> Submissions { get; set; }
    }
}
