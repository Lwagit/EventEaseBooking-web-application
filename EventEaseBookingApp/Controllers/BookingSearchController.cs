using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEaseBookingApp.Data;
using EventEaseBookingApp.Models;
using System.Linq;

namespace EventEaseBookingApp.Controllers
{
    public class BookingSearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingSearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookingSearch
        public IActionResult Index(string search)
        {
            var bookings = _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .Select(b => new BookingViewModel
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    VenueName = b.Venue.VenueName,
                    EventName = b.Event.EventName
                });

            if (!string.IsNullOrWhiteSpace(search))
            {
                bookings = bookings.Where(b =>
                    b.BookingId.ToString().Contains(search) ||
                    b.EventName.Contains(search) ||
                    b.VenueName.Contains(search)
                );
            }

            return View(bookings.ToList());
        }
    }
}