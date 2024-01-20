using BookingWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingWebApplication.Controllers
{
    public class ProvoliController : Controller
    {
        private readonly AppDBContext _dbContext;
        public ProvoliController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<IActionResult> Index(int? cinemasId, int? moviesId, int? id)
        {
            if (cinemasId == null || _dbContext.Cinemas == null ||
                moviesId == null || _dbContext.Movies == null ||
                id == null || _dbContext.ContentAdmins == null)
                return Notfound();

            var movie = await _dbContext.Movies
                .FirstOrDefaultAsync(m => m.MovieId == moviesId);

            var cinema = await _dbContext.Cinemas
                .FirstOrDefaultAsync(c => c.Id == cinemasId);

            if (movie == null || cinema == null)
                return Notfound();

            List<Provoli> provoles = await _dbContext.Provoles.Where(p => p.CinemasID == cinemasId && p.MoviesId == moviesId && p.MoviesName.Equals(movie.MovieName) && p.ContentAdminId == id).ToListAsync();

            if (!provoles.Any())
                return RedirectToAction("Notfound", "Provoli");

            Tuple<List<Provoli>, Cinema> tuple = new Tuple<List<Provoli>, Cinema>(provoles, cinema);
            return View(tuple);
        }

        public IActionResult Notfound()
        {
            return View();
        }
    }
}
