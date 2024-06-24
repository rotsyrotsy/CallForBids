// Controllers/AccountController.cs
using CallForBids.Models;
using CallForBids.Data;
using CallForBids.Models;
using CallForBids.Session;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MvcAdoExample.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Users
                {
                    Email = model.Email,
                    //Phone = model.Phone,
                    Password = model.Password, // TODO Note: Hash passwords in a real app!
                };

                var result = await _userRepository.RegisterUserAsync(user);
                if (result)
                {
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", "Failed to register user.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.LoginUserAsync(model.Email, model.Password);
                if (user != null)
                {
                    // Authenticate user (this is just an example, implement proper authentication)
                    HttpContext.Session.SetObject<Users>("User", user);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return RedirectToAction("Dish", "Index");

        }
    }
}
