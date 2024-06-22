using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CallForBids.Areas.RPSubmissions.Pages
{
    public class AcceptModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public AcceptModel(CallForBidsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Submissions Submissions { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var submissions =  await _context.Submissions
                .Include(s => s.Bid)
                .Include(s => s.User)
                .ThenInclude(u => u.Suppliers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submissions == null)
            {
                return NotFound();
            }
            Submissions = submissions;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var item = await _context.Submissions.FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return NotFound();
            }
            try
            {
                Submissions = item;
                if (Submissions.State!=1 && Submissions.State != 2)
                {
                    Submissions.State = 1;
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                else
                {
                    ErrorMessage = $"The submission has already been {(Submissions.State == 1 ? "accepted" : "rejected")}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return Page();
        }

    }
}
