using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CallForBids.Areas.RPOffices.Pages
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
            return Page();
        }

        [BindProperty]
        public Offices Offices { get; set; } = new();
        public string ErrorMessage { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var item = new Offices();
            if (await TryUpdateModelAsync(item, "Office", o=>o.Location,o=>o.Code,o=>o.Address))
            {
                _context.Offices.Add(item);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
            .SelectMany(E => E.Errors)
            .Select(E => E.ErrorMessage)
            .ToList();
            if (validationErrors.Count>0)
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
