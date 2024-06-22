using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPSuppliers.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public DetailsModel(CallForBidsContext context)
        {
            _context = context;
        }

        public Suppliers Suppliers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var suppliers = await _context.Suppliers
                .AsNoTracking()
                .Include(s => s.User)
                .ThenInclude(u => u.Submissions)
                .ThenInclude(su => su.Bid)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (suppliers == null)
            {
                return NotFound();
            }
            else
            {
                Suppliers = suppliers;
            }
            return Page();

        }
    }
}
