using CallForBids.Data;
using CallForBids.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CallForBids.Areas.RPProjects.Pages
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
            return Page();
        }

        [BindProperty]
        public Projects Projects { get; set; } = new();
        public IFormFile Upload { get; set; }
        public string ErrorMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostFormProjectsAsync()
        {
            var newProject = new Projects();
            if (await TryUpdateModelAsync(newProject, "Project", f => f.Title, f => f.Poet))
            {
                _context.Projects.Add(newProject);
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
                List<Projects> projects;
                if (Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    var csvReader = new ProjectCsvReader();
                    projects = csvReader.ReadCsvFile(file);
                }
                else
                {
                    throw new Exception("Unsupported file format");
                }
                // Insert projects into the database
                _context.Projects.AddRange(projects);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch(Exception e)
            {
                ErrorMessage = e.Message;
                return Page();
            }
            
        }
    }
}
