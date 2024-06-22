using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPProjects.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public IndexModel(CallForBidsContext context)
        {
            _context = context;
        }

        public IList<Projects> Projects { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Projects = await _context.Projects.ToListAsync();
        }
    }
}
