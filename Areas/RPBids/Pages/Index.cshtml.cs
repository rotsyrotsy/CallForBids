using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPBids.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public IndexModel(CallForBidsContext context)
        {
            _context = context;
        }

        public IList<Bids> Bids { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Bids = await _context.Bids
                .Include(b => b.Office)
                .Include(b => b.Project)
                .Include(b => b.Submissions)
                .ToListAsync();
        }
    }
}
