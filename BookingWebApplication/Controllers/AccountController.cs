using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingWebApplication.Models;

namespace BookingWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext _context;

        public AccountController(AppDBContext context)
        {
            _context = context;
        }

        public override ViewResult View(string? viewName, object? model)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                this.ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
                this.ViewBag.UserName = HttpContext.Session.GetString("UserName").ToString();
                this.ViewBag.UserRole = HttpContext.Session.GetString("UserRole").ToString();
            }
            return base.View(viewName, model);
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Email,Password,Role")] User user)
        {
            user.CreateTime = DateTime.Now;
            user.Salt = RandomString(3);
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // Post: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,Email,Password")] User user)
        {
            user.CreateTime = DateTime.Now;
            user.Salt = RandomString(3);
            user.Role = "Customer";
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();

                Customer customer = new Customer { UserName = user.UserName };
                _context.Add(customer);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("UserSession", user.Email);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("UserRole", "Customer");
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(string id)
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

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserName,Email,Password,CreateTime,Salt,Role")] User user)
        {
            if (id != user.UserName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserName))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Account/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.UserName == id);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var userFromDb = _context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

            if (userFromDb != null)
            {
                // Successful login, redirect to a secure area
                HttpContext.Session.SetString("UserSession", userFromDb.Email);
                HttpContext.Session.SetString("UserName", user.UserName);

                Admin admin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.UserName.Equals(user.UserName));

                ContentAdmin contentAdmin = await _context.ContentAdmins
                    .FirstOrDefaultAsync(c => c.UserName.Equals(user.UserName));

                if (admin != null)
                    HttpContext.Session.SetString("UserRole", "Admin");
                else if (contentAdmin != null)
                    HttpContext.Session.SetString("UserRole", "ContentAdmin");
                else
                    HttpContext.Session.SetString("UserRole", "Customer");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Failed login, display an error message
                ViewBag.Error = "Invalid username or password";
                return View();
            }
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
            }
            return RedirectToAction("Index", "Home");
        }

        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
