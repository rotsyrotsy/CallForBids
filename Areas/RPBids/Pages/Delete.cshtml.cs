using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CallForBids.Data;
using CallForBids.Models;

namespace CallForBids.Areas.RPBids.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public DeleteModel(CallForBidsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bids Bids { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? hasErrorMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bids = await _context.Bids.
                AsNoTracking().
                FirstOrDefaultAsync(m => m.Id == id);

            if (bids == null)
            {
                return NotFound();
            }
            else
            {
                Bids = bids;
            }
            if (hasErrorMessage.GetValueOrDefault())
            {
                ErrorMessage = $"Error on bids {Bids.Title} ({Bids.Id}) deletion";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var bid = await _context.Bids.Include(b => b.Submissions).FirstOrDefaultAsync(m => m.Id == id);
            if (bid == null)
            {
                return NotFound();
            }
            try
            {
                Bids = bid;
                _context.Bids.Remove(Bids);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
                return RedirectToPage("./Delete", new {id, hasErrorMessage =true});
            }
        }
    }
}
