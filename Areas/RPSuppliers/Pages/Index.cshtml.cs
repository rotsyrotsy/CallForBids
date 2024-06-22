using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPSuppliers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public IndexModel(CallForBidsContext context)
        {
            _context = context;
        }

        public IList<Suppliers> Suppliers { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Suppliers = await _context.Suppliers.ToListAsync();
        }
    }
}
