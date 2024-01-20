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
    public class ContentAdminsController : Controller
    {
        private readonly AppDBContext _context;

        public ContentAdminsController(AppDBContext context)
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

        // GET: ContentAdmins
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.ContentAdmins.Include(c => c.User);
            return View(await appDBContext.ToListAsync());
        }

        // GET: ContentAdmins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentAdmin == null)
            {
                return NotFound();
            }

            return View(contentAdmin);
        }

        // GET: ContentAdmins/Create
        public IActionResult Create()
        {
            ViewData["UserName"] = new SelectList(_context.Users, "UserName", "UserName");
            return View();
        }

        // POST: ContentAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UserName")] ContentAdmin contentAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = new SelectList(_context.Users, "UserName", "UserName", contentAdmin.UserName);
            return View(contentAdmin);
        }

        // GET: ContentAdmins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins.FindAsync(id);
            if (contentAdmin == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = new SelectList(_context.Users, "UserName", "UserName", contentAdmin.UserName);
            return View(contentAdmin);
        }

        // POST: ContentAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserName")] ContentAdmin contentAdmin)
        {
            if (id != contentAdmin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentAdminExists(contentAdmin.Id))
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
            ViewData["UserName"] = new SelectList(_context.Users, "UserName", "UserName", contentAdmin.UserName);
            return View(contentAdmin);
        }

        // GET: ContentAdmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentAdmin == null)
            {
                return NotFound();
            }

            return View(contentAdmin);
        }

        // POST: ContentAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentAdmin = await _context.ContentAdmins.FindAsync(id);
            if (contentAdmin != null)
            {
                _context.ContentAdmins.Remove(contentAdmin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentAdminExists(int id)
        {
            return _context.ContentAdmins.Any(e => e.Id == id);
        }

        // GET: ContentAdmins/CreateProvoli
        public async Task<IActionResult> CreateProvoli()
        {
            if (HttpContext.Session.GetString("UserRole") == null)
                return RedirectToAction("Index", "ContentAdmins");

            if (!HttpContext.Session.GetString("UserRole").Equals("ContentAdmin"))
                return RedirectToAction("Index", "ContentAdmins");

            ViewBag.Cinemas = new SelectList(_context.Cinemas, "Id", "Name");
            ViewBag.Movies = new SelectList(_context.Movies, "MovieId", "MovieName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProvoli(Provoli provoli)
        {
            int.TryParse(provoli.MoviesName, out int moviesId);
            if (moviesId == null)
                return NotFound();

            Movie movie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == moviesId);
            Cinema cinema = await _context.Cinemas.FindAsync(provoli.CinemasID);
            ContentAdmin contentAdmin = await _context.ContentAdmins.FirstOrDefaultAsync(c => c.UserName.Equals(HttpContext.Session.GetString("UserName").ToString()));

            if (movie == null || cinema == null || contentAdmin == null)
            {
                ModelState.AddModelError("", "Invalid movie or cinema.");
                return NotFound();
            }

            provoli.MoviesName = movie.MovieName;
            provoli.MoviesId = movie.MovieId;
            provoli.CinemasID = cinema.Id;
            provoli.ContentAdminId = contentAdmin.Id;

            if (ModelState.IsValid)
            {
                _context.Provoles.Add(provoli);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Cinemas = new SelectList(_context.Cinemas, "Id", "Name");
            ViewBag.Movies = new SelectList(_context.Movies, "MovieId", "MovieName");
            return View(provoli);
        }

        // GET: ContentAdmins/CreateMovie
        public async Task<IActionResult> CreateMovie()
        {
            if (HttpContext.Session.GetString("UserRole") == null)
                return RedirectToAction("Index", "ContentAdmins");

            if (!HttpContext.Session.GetString("UserRole").Equals("ContentAdmin"))
                return RedirectToAction("Index", "ContentAdmins");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovie(Movie movie)
        {
            ContentAdmin contentAdmin = await _context.ContentAdmins.FirstOrDefaultAsync(c => c.UserName.Equals(HttpContext.Session.GetString("UserName").ToString()));

            if (contentAdmin == null)
                return NotFound();

            movie.ContentAdminId = contentAdmin.Id;

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Movie");

           /* if (ModelState.IsValid)
            {

            }

            ViewBag.Error = "Could not add movie. Something went wrong.";
            return View(movie);*/
        }
    }
}
