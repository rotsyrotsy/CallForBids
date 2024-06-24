using Microsoft.AspNetCore.Mvc;
using CallForBids.Data;
using CallForBids.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CallForBids.Session;
using CallForBids.Models;
using CallForBids.Session;
using NuGet.ContentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CallForBids.Controllers
{
    public class BidsController : Controller
    {
        private readonly BidsRepository _BidsRepo;
        private readonly SubmissionRepository _subRepo;

        public BidsController(BidsRepository BidsRepository, SubmissionRepository submissionRepo)
        {
            _BidsRepo = BidsRepository;
            _subRepo = submissionRepo;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, string searchTerm = null, int? officeId = null)
        {
            // Example: Verify if session user is present
            var user = HttpContext.Session.GetObject<Users>("User");
            if (user == null)
            {
                // If user is not logged in, redirect to login page
                return RedirectToAction("Login", "User");
            }

            var Bidses = await _BidsRepo.GetBidsesAsync(pageNumber, 10, searchTerm, officeId);
            var totalItems = await _BidsRepo.GetTotalBidsesCountAsync(searchTerm, officeId);

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = 10;
            ViewBag.TotalItems = totalItems;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.OfficeId = officeId;

            return View(Bidses);
        }
     
        [HttpPost]
        public async Task<IActionResult> SubmitToBid(Submissions sub)
        {
            // Example: Verify if session user is present
            var user = HttpContext.Session.GetObject<Users>("User");
            if (user == null)
            {
                // If user is not logged in, redirect to login page
                return RedirectToAction("Login", "User");
            }

            if (ModelState.IsValid)
            {
                // Process the data
                var description = sub.Description;
                var bidId = sub.BidId;


                // You can add your logic here
                Submissions submis = new Submissions();
                submis.BidId = bidId;
                submis.UserId = user.Id;
                submis.Description = description;
                submis.State = 0;
                submis.Date = DateTime.Now;

                await _subRepo.insertSubmissionAsync(submis);
            }


            return RedirectToAction("Index", "Bids"); // Adjust as per your application flow
        }

        public async Task<IActionResult> Submission(int bidId)
        {
            //Example: Verify if session user is present
            var user = HttpContext.Session.GetObject<Users>("User");
            if (user == null)
            {
                // If user is not logged in, redirect to login page
                return RedirectToAction("Login", "User");
            }

            ViewBag.bidId = bidId;

            return View();

        }

    }
}
