using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPSubmissions.Pages
{
    public class ListModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public ListModel(CallForBidsContext context)
        {
            _context = context;
        }

        public IList<Submissions> Submissions { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Submissions = await _context.Submissions
                .Where(s => s.State == SubmissionState.Pending)
                .Include(s => s.Bid)
                .Include(s => s.User)
                .ThenInclude(u => u.Suppliers).ToListAsync();
        }
    }
}
