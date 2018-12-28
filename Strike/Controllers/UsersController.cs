using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Strike.Data;
using Strike.Models;

namespace Strike.Controllers
{
    public class UsersController : Controller
    {
        private readonly StrikeContext _context;

        public UsersController(StrikeContext context)
        {
            _context = context;
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

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("FirstName,LastName,Email,Password")] User user)
        {
            bool userExists = _context.Users.Any(u => u.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase));

            if (userExists)
            {
                ViewData.Add("error", "E-postadressen existerar redan!");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = hashedPassword;

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(user);
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            User user = _context.Users.SingleOrDefault(u => u.Email.Equals(Email, StringComparison.InvariantCultureIgnoreCase));
            if (user == null) //No existing user with this email
            {
                ViewData.Add("error", "Felaktig e-postadress eller lösenord!");
                return View();
            }

            bool passwordMatches = BCrypt.Net.BCrypt.Verify(Password, user.Password);
            if (!passwordMatches)//Password does not match
            {
                ViewData.Add("error", "Felaktig e-postadress eller lösenord!");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Email),
                new Claim(Models.User.UserId, user.Id.ToString())
            };

            var userIdentity = new ClaimsIdentity(claims, "login");

            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Users");
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(string FirstName, string LastName, string Email, string Phone)
        {
            var identity = (ClaimsIdentity)User.Identity;
            int userId = Convert.ToInt32(identity.FindFirst(Models.User.UserId).Value);
            User loggedInUser = await _context.Users.FindAsync(userId);

            bool hasChanged = 
                !FirstName.Equals(loggedInUser.FirstName) ||
                !LastName.Equals(loggedInUser.LastName) ||
                !Email.Equals(loggedInUser.Email) ||
                !Phone.Equals(loggedInUser.Phone);

            if (hasChanged)
            {
                loggedInUser.FirstName = FirstName;
                loggedInUser.LastName = LastName;
                loggedInUser.Email = Email;
                loggedInUser.Phone = Phone;

                _context.Update(loggedInUser);
                await _context.SaveChangesAsync();
                ViewData.Add("changed", "Dina inställningar är nu uppdaterade!");
            }

            return View(loggedInUser);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        [HttpPost, ActionName("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(int id, [FromForm] ChangePasswordForm changePasswordForm)
        {
            string referer = Request.Headers["Referer"].ToString();
            var identity = (ClaimsIdentity)User.Identity;
            int userId = Convert.ToInt32(identity.FindFirst(Models.User.UserId).Value);
            User loggedInUser = await _context.Users.FindAsync(userId);

            bool passwordMatches = BCrypt.Net.BCrypt.Verify(changePasswordForm.CurrentPassword, loggedInUser.Password);

            if (!passwordMatches)
            {
                ViewData.Add("error", "Felaktigt lösenord!");
                return View("Edit", loggedInUser);
            }

            if (!changePasswordForm.NewPassword.Equals(changePasswordForm.RepeatNewPassword))
            {
                ViewData.Add("error", "Lösenorden matchar inte!");
                return View("Edit", loggedInUser);
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordForm.NewPassword);
            loggedInUser.Password = hashedPassword;

            _context.Update(loggedInUser);
            await _context.SaveChangesAsync();
            ViewData.Add("success", "Lösenordet är nu ändrat!");
            return View("Edit", loggedInUser);
        }
    }
}
