using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingWebApplication.Models;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Authorization;

namespace BookingWebApplication.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDBContext _dbContext;
        public ReservationController(AppDBContext dbContext)
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> History()
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Login", "Account");

            Customer customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.UserName == HttpContext.Session.GetString("UserName").ToString());

            if (customer == null)
                return RedirectToAction("Index", "Home");

            List<Reservation> reservations = await _dbContext.Reservations.Where(r => r.CustomersId == customer.Id).ToListAsync();

            return View(reservations);
        }

        public async Task<IActionResult> Create(int? cinemasId, int? moviesId, int? contentAdminsId, int? provolesId)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
                return RedirectToAction("Login", "Account");

            if (!HttpContext.Session.GetString("UserRole").Equals("Customer"))
                return RedirectToAction("Index", "Home");

            if (cinemasId == null || _dbContext.Cinemas == null ||
                moviesId == null || _dbContext.Movies == null ||
                contentAdminsId == null || _dbContext.ContentAdmins == null ||
                provolesId == null || _dbContext.Provoles == null)
                return NotFound();

            var movie = await _dbContext.Movies
                .FirstOrDefaultAsync(m => m.MovieId == moviesId);

            var cinema = await _dbContext.Cinemas
                .FirstOrDefaultAsync(c => c.Id == cinemasId);

            if (movie == null || cinema == null)
                return NotFound();

            var provoli = await _dbContext.Provoles
                .FirstOrDefaultAsync(p => p.CinemasID == cinemasId && p.MoviesId == moviesId && p.MoviesName.Equals(movie.MovieName) && p.ContentAdminId == contentAdminsId && p.Id == provolesId);

            List<Reservation> reservations = new List<Reservation>();
            reservations = await _dbContext.Reservations
                .Where(r => r.ProvolesMoviesId == moviesId && r.ProvolesMoviesName.Equals(movie.MovieName) && r.ProvolesContentAdminId == contentAdminsId && r.ProvolesDateTime == provoli.ShowDateTime)
                .ToListAsync();

            Tuple<Provoli, Movie, Cinema, List<Reservation>> tuple = new Tuple<Provoli, Movie, Cinema, List<Reservation>>(provoli, movie, cinema, reservations);
            return View(tuple);
        }

        [RequestFormLimits(ValueLengthLimit = 50000000, MultipartBodyLengthLimit = 50000000)]
        [HttpPost]
        public async Task<IActionResult> Create(
            int ProvolesMoviesId,
            string ProvolesMoviesName,
            int ProvolesCinemasId,
            DateTime ProvolesDateTime,
            string Seats)
        {
            if (ProvolesMoviesId == null || ProvolesMoviesName == null || ProvolesCinemasId == null || ProvolesDateTime == null || Seats == null)
            {
                return NotFound();
            }

            Customer customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.UserName == HttpContext.Session.GetString("UserName").ToString());

            if (customer == null)
                return NotFound();


            DateTime dateTime;
            dateTime = new DateTime(
                ProvolesDateTime.Ticks - (ProvolesDateTime.Ticks % TimeSpan.TicksPerSecond),
                ProvolesDateTime.Kind
            );

            Reservation reservation = new Reservation();
            reservation.ProvolesMoviesId = ProvolesMoviesId;
            reservation.ProvolesMoviesName = ProvolesMoviesName;
            reservation.ProvolesDateTime = dateTime;
            reservation.ProvolesCinemasId = ProvolesCinemasId;
            reservation.CustomersId = customer.Id;
            reservation.Seats = Seats;

            string[] seatList = reservation.Seats.Split(' ');
            int count = 0;
            foreach (var seat in seatList)
            {
                if (int.TryParse(seat, out int seat_int))
                {
                    count++;
                }
            }
            reservation.NumberOfSeats = count;

            if (ModelState.IsValid)
            {
                _dbContext.Add(reservation);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("History", "Reservation");
            }

            return View();
        }
    }
}
