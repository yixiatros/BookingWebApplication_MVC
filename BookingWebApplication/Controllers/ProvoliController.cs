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

        public async Task<IActionResult> Index(int? cinemasId, int? moviesId, int? id)
        {
            if (cinemasId == null || _dbContext.Cinemas == null ||
                moviesId == null || _dbContext.Movies == null ||
                id == null || _dbContext.ContentAdmins == null)
                return NotFound();

            var movie = await _dbContext.Movies
                .FirstOrDefaultAsync(m => m.MovieId == moviesId);

            var cinema = await _dbContext.Cinemas
                .FirstOrDefaultAsync(c => c.Id == cinemasId);

            if (movie == null || cinema == null)
                return NotFound();

            var provoli = await _dbContext.Provoles
                .FirstOrDefaultAsync(p => p.CinemasID == cinemasId && p.MoviesId == moviesId && p.MoviesName.Equals(movie.MovieName) && p.ContentAdminId == id);

            if (provoli == null)
                return NotFound();

            Tuple<Provoli, Cinema> tuple = new Tuple<Provoli, Cinema>(provoli, cinema);
            return View(tuple);
        }
    }
}
