using BookingWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingWebApplication.Controllers
{
    public class CinemaController : Controller
    {
        private readonly AppDBContext _dbContext;
        public CinemaController(AppDBContext dbContext)
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

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Cinemas.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _dbContext.Cinemas == null)
                return NotFound();

            var cinema = await _dbContext.Cinemas
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cinema == null)
                return NotFound();

            Tuple<Cinema, List<Provoli>, List<Movie>> tuple =
                new Tuple<Cinema, List<Provoli>, List<Movie>> (
                    cinema,
                    await _dbContext.Provoles.ToListAsync(),
                    await _dbContext.Movies.ToListAsync()
                    );
            return View(tuple);
        }
    }
}
