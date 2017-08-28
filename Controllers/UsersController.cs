using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travels.Data;
using Travels.Models;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Travels.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly TravelContext _context;
        private Claim user;

        public ClaimsIdentity Email { get; private set; }

        public UsersController(TravelContext context)
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

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        [AllowAnonymous]
        [Route("register")]
        public IActionResult Create()
        {
            user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                return View();
            }else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Create([Bind("ID,Email,Password")] Users users)
        {
            user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            bool AlreadyExist = _context.Users.Any(u => u.Email == users.Email);
            if (AlreadyExist)
            {
                ViewBag.AlreadyExist = true;
                return View("~/Views/Users/Create.cshtml");
            }
            if (user == null)
            {
                if (ModelState.IsValid)
                {
                    users.Password = getHash(users.Password);
                    _context.Add(users);
                    await _context.SaveChangesAsync();
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, users.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Name, users.Email));

                    // Authenticate using the identity
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                }
                return RedirectToAction("Index", "Tours");
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
           
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var editableUser = _context.Users
                    .Where(b => b.ID == id)
                    .FirstOrDefault();
            if (id == null || (user != null && editableUser != null && user.Value != editableUser.Email) )
            { 
                return NotFound();
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Email,Password")] Users users)
        {
            user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userq = _context.Users.Where(u => u.Email == user.Value).AsNoTracking().FirstOrDefault();
            bool AlreadyExist = _context.Users.Any(u => u.Email == users.Email);
            if (AlreadyExist)
            {
                ViewBag.AlreadyExist = true;

                return View("~/Views/Users/Edit.cshtml", users);
            }
            if (users.ID != id || (user != null && userq.ID != users.ID))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    users.Password = getHash(users.Password);
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Logout", "Login");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var removableUser = _context.Users
                    .Where(b => b.ID == id)
                    .FirstOrDefault();
            if (id == null || (user != null && removableUser != null && user.Value != removableUser.Email))
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var users = await _context.Users.SingleOrDefaultAsync(m => m.ID == id);

            if (users != null && users.Email == user.Value)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _context.Users.Remove(users);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
