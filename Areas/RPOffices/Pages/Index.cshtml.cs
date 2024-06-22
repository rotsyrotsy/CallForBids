using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPOffices.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public IndexModel(CallForBidsContext context)
        {
            _context = context;
        }

        public IList<Offices> Offices { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Offices = await _context.Offices.ToListAsync();
        }
    }
}
