using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MusicShop.Services;
using MusicShop.Entities;
using MusicShop.Data;
using MusicShop.Models;

namespace MusicShop.Controllers
{
    public class AccountController : Controller
    {
        readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterModel();
            return View(response);
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            try
            {
                _userService.RegisterUser(registerViewModel.Username, registerViewModel.Password,
                    registerViewModel.FullName, registerViewModel.EmailAddress);
            }
            catch(ArgumentException ex) 
            {
                TempData["Error"] = ex.Message;
                return View(registerViewModel);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            var response = new LoginModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            User? user;

            if (!_userService.VerifyUser(loginViewModel.Username, loginViewModel.Password, out user))
            {
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginViewModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties { AllowRefresh = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),authProperties);

            //if (_userService.HasBankAccount(user.Id))
            //    return RedirectToAction("Index", "BankAccount");

            return RedirectToAction("Index", "Shop");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
