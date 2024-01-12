using BookingWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingWebApplication.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDBContext _dbContext;
        public MovieController (AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Movie> objMoviesList = _dbContext.Movies.ToList();
            return View(objMoviesList);
        }
    }
}
