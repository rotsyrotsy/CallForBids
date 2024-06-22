using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CallForBids.Data;
using CallForBids.Models;

namespace CallForBids.Areas.RPSubmissions.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly CallForBidsContext _context;

        public DetailsModel(CallForBidsContext context)
        {
            _context = context;
        }

        public Submissions Submissions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var submissions = await _context.Submissions
                .AsNoTracking()
                .Include(s => s.Bid)
                .Include(s => s.User)
                .ThenInclude(u => u.Suppliers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (submissions == null)
            {
                return NotFound();
            }
            else
            {
                Submissions = submissions;
            }
            return Page();
        }
    }
}
