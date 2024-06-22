using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CallForBids.Data;
using CallForBids.Models;

namespace CallForBids.Areas.RPBids.Pages
{
    public class EditModel : PageModel
    {
        private readonly CallForBids.Data.CallForBidsContext _context;

        public EditModel(CallForBids.Data.CallForBidsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bids Bids { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bids =  await _context.Bids.FirstOrDefaultAsync(m => m.Id == id);
            if (bids == null)
            {
                return NotFound();
            }
            Bids = bids;
           ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Location");
           ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Bids).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidsExists(Bids.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BidsExists(int id)
        {
            return _context.Bids.Any(e => e.Id == id);
        }
    }
}
