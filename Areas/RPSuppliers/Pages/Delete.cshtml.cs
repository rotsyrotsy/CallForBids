using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CallForBids.Areas.RPSuppliers.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public DeleteModel(CallForBidsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Suppliers Suppliers { get; set; } 
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? hasErrorMessage = false)
        {
            var suppliers = await _context.Suppliers.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (suppliers == null)
            {
                return NotFound();
            }
            else
            {
                Suppliers = suppliers;
            }
            if (hasErrorMessage.GetValueOrDefault())
            {
                ErrorMessage = $"Error on supplier {Suppliers.Name} deletion";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            var suppliers = await _context.Suppliers.FirstOrDefaultAsync(m => m.Id == id);

            if (suppliers == null)
            {
                return NotFound();
            }
            try
            {
                Suppliers = suppliers;
                _context.Suppliers.Remove(Suppliers);
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
