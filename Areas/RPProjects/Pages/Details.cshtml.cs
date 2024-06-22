using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPProjects.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public DetailsModel(CallForBidsContext context)
        {
            _context = context;
        }

        public Projects Projects { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var projects = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (projects == null)
            {
                return NotFound();
            }
            else
            {
                Projects = projects;
            }

            return Page();
        }
    }
}
