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
        private IWebHostEnvironment _environment;

        public CreateModel(CallForBidsContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
        public IFormFile Upload { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostFormBidsAsync()
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
        public async Task<IActionResult> OnPostFormUploadAsync()
        {
            try
            {
                var file = Path.Combine(_environment.ContentRootPath, "wwwroot/uploads", Upload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
                List<Bids> bids;
                if (Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    var csvReader = new BidCsvReader(_context);
                    bids = csvReader.ReadCsvFile(file);
                }
                else
                {
                    throw new Exception("Unsupported file format");
                }
                // Insert projects into the database
                _context.Bids.AddRange(bids);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return Page();
            }

        }
    }
}
