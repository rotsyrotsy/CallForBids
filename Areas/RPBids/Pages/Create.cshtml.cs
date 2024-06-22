using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CallForBids.Areas.RPBids.Pages
{
    public class CreateModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public CreateModel(CallForBidsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Location");
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Bids Bids { get; set; } = new();
        public string ErrorMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var newItem = new Bids();

            if (await TryUpdateModelAsync(newItem, "Bid"))
            {
                _context.Bids.Add(newItem);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
                var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
            .SelectMany(E => E.Errors)
            .Select(E => E.ErrorMessage)
            .ToList();
            if (validationErrors.Count > 0)
            {
                foreach (var error in validationErrors)
                {
                    ErrorMessage += $"{error}\n";
                }
            }
            return Page();
        }
    }
}
