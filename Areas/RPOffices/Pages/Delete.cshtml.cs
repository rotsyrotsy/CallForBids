using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CallForBids.Areas.RPOffices.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public DeleteModel(CallForBidsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Offices Offices { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string? errorMessage = null)
        {

            var item = await _context.Offices.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                Offices = item;
            }
            if (!errorMessage.IsNullOrEmpty())
            {
                ErrorMessage = $"Error on office {Offices.Location} deletion : {errorMessage}";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var item = await _context.Offices.FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return NotFound();
            }
            try
            {
                Offices = item;
                _context.Offices.Remove(Offices);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return RedirectToPage("./Delete", new { id, errorMessage = ex.Message });
            }
        }
    }
}
