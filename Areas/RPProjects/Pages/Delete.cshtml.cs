using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPProjects.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public DeleteModel(CallForBidsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Projects Projects { get; set; } = default!;
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? hasErrorMessage = false)
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
            if (hasErrorMessage.GetValueOrDefault())
            {
                ErrorMessage = $"Error on project {Projects.Title} deletion";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            var projects = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);

            if (projects == null)
            {
                return NotFound();
            }
            try
            {
                Projects = projects;
                _context.Projects.Remove(Projects);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
                return RedirectToPage("./Delete", new { id, hasErrorMessage = true });
            }
        }
    }
}
