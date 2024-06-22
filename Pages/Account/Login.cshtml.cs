using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CallForBids.Models;
using Microsoft.EntityFrameworkCore;
using CallForBids.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CallForBids.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [TempData]
        public string ErrorMessage { get; set; }
        public string ReturnUrl { get; set; }
        [BindProperty, Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        private readonly CallForBidsContext _context;

        public LoginModel(CallForBidsContext context)
        {
            _context = context;
        }

        public void OnGet(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var userExists = await _context.Users
                .AnyAsync(u => u.Email == Email && u.Password == Password);

                var verificationResult = userExists; // TODO: Verify username and password

                if (verificationResult)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Email)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return Redirect(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
