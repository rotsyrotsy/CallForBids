// Controllers/AccountController.cs
using CallForBids.Models;
using CallForBids.Data;
using CallForBids.Models;
using CallForBids.Session;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
                if (model.ConfirmPassword == model.Password)
                {
                    var supplier = new Suppliers
                    {
                        Name = model.Name,
                        SupplierNumber = model.SupplierNumber,
                        Address = model.Address,
                        Phone = model.Phone,
                        Email = model.Email,
                    };

                    var idSupplier = await _userRepository.RegisterSupplierAsync(supplier);
                    if (idSupplier > 0)
                    {
                        var user = new Users
                        {
                            Email = model.Email,
                            Password = model.Password,
                            SupplierId = idSupplier,
                        };
                        var result = await _userRepository.RegisterUserAsync(user);
                        if (result)
                        {
                            return RedirectToAction("Login");
                        }
                    }
                    ModelState.AddModelError("", "Failed to register user.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Password and confirm password do not match.");
                }
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
                    return RedirectToAction("Index", "Bids");
                }
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();

        }
    }
}
