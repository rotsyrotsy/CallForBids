using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPOffices.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public DetailsModel(CallForBidsContext context)
        {
            _context = context;
        }

        public Offices Offices { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offices = await _context.Offices.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (offices == null)
            {
                return NotFound();
            }
            else
            {
                Offices = offices;
            }
            return Page();
        }
    }
}
