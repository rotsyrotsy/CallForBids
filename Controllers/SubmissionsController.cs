using CallForBids.Data;
using CallForBids.Models;
using CallForBids.Services;
using CallForBids.Session;
using Microsoft.AspNetCore.Mvc;

namespace CallForBids.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly SubmissionRepository _subRepo;
        private readonly PdfService _pdfService;
        private readonly RazorViewToStringRenderer _razorViewToStringRenderer;

        public SubmissionsController( SubmissionRepository submissionRepo, PdfService pdfService, RazorViewToStringRenderer razorViewToStringRenderer)
        {
            _subRepo = submissionRepo;
            _pdfService = pdfService;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }
        public async Task<IActionResult> Index()
        {
            // Example: Verify if session user is present
            var user = HttpContext.Session.GetObject<Users>("User");
            if (user == null)
            {
                // If user is not logged in, redirect to login page
                return RedirectToAction("Login", "User");
            }

            var submissions = await _subRepo.GetSubmissionsAsync(user.Id);

            return View(submissions);
        }
        [HttpGet]
        public async Task<IActionResult> Export()
        {
            // Example: Verify if session user is present
            var user = HttpContext.Session.GetObject<Users>("User");

            if (user == null)
            {
                // If user is not logged in, redirect to login page
                return RedirectToAction("Login", "Account");
            }

            List<Submissions> submissions = await _subRepo.GetSubmissionsAsync(user.Id);


            var model = new ExportSubmissionsToPdf
            {
                Users = user,
                Submissions = submissions
            };


            string htmlContent = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Pdf/ExportSubmissionsToPdf.cshtml", model);
            var pdfBytes = _pdfService.CreatePdf(htmlContent);


            return File(pdfBytes, "application/pdf", "ExportSubmissions.pdf");

        }
    }
}
