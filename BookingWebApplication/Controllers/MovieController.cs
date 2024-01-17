using BookingWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookingWebApplication.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDBContext _dbContext;
        public MovieController (AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override ViewResult View(string? viewName, object? model)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                this.ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            return base.View(viewName, model);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Movies.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _dbContext.Movies == null)
                return NotFound();

            var movie = await _dbContext.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
                return NotFound();

            Tuple<Movie, List<Cinema>> tuple = new Tuple<Movie, List<Cinema>>(movie, await _dbContext.Cinemas.ToListAsync());
            return View(tuple);
        }
    }
}
