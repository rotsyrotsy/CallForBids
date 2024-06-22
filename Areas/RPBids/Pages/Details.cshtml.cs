using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPBids.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public DetailsModel(CallForBidsContext context)
        {
            _context = context;
        }

        public Bids Bids { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var bids = await _context.Bids.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (bids == null)
            {
                return NotFound();
            }
            else
            {
                Bids = bids;
            }
            return Page();
        }
    }
}
