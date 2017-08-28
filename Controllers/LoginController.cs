using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Travels.Data;
using Travels.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Travels.Controllers
{
    public class LoginController : Controller
    {
        private readonly TravelContext _context;

        public LoginController(TravelContext context)
        {
            _context = context;
        }

        private static string getHash(string text)
        {
            // SHA512 is disposable by inheritance.
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Get the hashed string.
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // GET: /<controller>/
        [HttpGet]
        [Route("login")]
        public IActionResult Index()
        {
            var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null) return View();
            return View("~/Views/Home/index.cshtml");
            
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Authenticate(string Email, string Password)
        {
            var user =  _context.Users
                .Where(u => u.Email == Email)
                .FirstOrDefault();
            
            if (user != null && user.Password == getHash(Password))
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Email));
                identity.AddClaim(new Claim(ClaimTypes.Name, Email));

                // Authenticate using the identity
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Tours");
            }
            else
            {
                ViewBag.NotMatch = true;
                return View("~/Views/Login/Index.cshtml");
            }
        }

        [Route("/logout")]
        public IActionResult Logout(string Email, string Password)
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
