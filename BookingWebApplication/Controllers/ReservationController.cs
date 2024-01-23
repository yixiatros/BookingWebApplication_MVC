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
            List<Provoli> provoles = await _dbContext.Provoles.ToListAsync();

            Tuple<List<Reservation>, List<Provoli>> tuple = new Tuple<List<Reservation>, List<Provoli>>(reservations, provoles);
            return View(tuple);
        }

        [HttpGet]
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

            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.UserName == HttpContext.Session.GetString("UserName").ToString());
            var hasAlreadyReservation = await _dbContext.Reservations
                .FirstOrDefaultAsync(r => r.ProvolesMoviesId == moviesId
                && r.ProvolesMoviesName.Equals(movie.MovieName)
                && r.ProvolesContentAdminId == contentAdminsId
                && r.ProvolesId == provoli.Id
                && r.CustomersId == customer.Id
                );
            if (hasAlreadyReservation != null)
            {
                ViewBag.Error = "You already have made a reservation for this screening/projection.";
                return RedirectToAction("Index", "Home");
            }

            List<Reservation> reservations = new List<Reservation>();
            reservations = await _dbContext.Reservations
                .Where(r => r.ProvolesMoviesId == moviesId && r.ProvolesMoviesName.Equals(movie.MovieName) && r.ProvolesContentAdminId == contentAdminsId && r.ProvolesId == provoli.Id)
                .ToListAsync();

            Tuple<Provoli, Movie, Cinema, List<Reservation>> tuple = new Tuple<Provoli, Movie, Cinema, List<Reservation>>(provoli, movie, cinema, reservations);
            return View(tuple);
        }

        [RequestFormLimits(ValueLengthLimit = 50000000, MultipartBodyLengthLimit = 50000000)]
        [HttpPost]
        public async Task<IActionResult> Create(
            int ProvolesId,
            int ProvolesMoviesId,
            string ProvolesMoviesName,
            int ProvolesCinemasId,
            int ProvolesContentAdminId,
            string Seats)
        {
            if (ProvolesId == null
                || ProvolesMoviesId == null
                || ProvolesMoviesName == null
                || ProvolesCinemasId == null
                || ProvolesContentAdminId == null
                || Seats == null)
            {
                return NotFound();
            }

            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.UserName == HttpContext.Session.GetString("UserName").ToString());

            if (customer == null)
                return NotFound();

            var provoli = await _dbContext.Provoles
                .FirstOrDefaultAsync(p => p.Id ==  ProvolesId);

            if (provoli == null)
                return NotFound();

            Reservation reservation = new Reservation();
            reservation.ProvolesId = ProvolesId;
            reservation.ProvolesMoviesId = ProvolesMoviesId;
            reservation.ProvolesMoviesName = ProvolesMoviesName;
            reservation.ProvolesCinemasId = ProvolesCinemasId;
            reservation.ProvolesContentAdminId = ProvolesContentAdminId;
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
