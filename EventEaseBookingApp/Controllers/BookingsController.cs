using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEaseBookingApp.Models;
using EventEaseBookingApp.Data;
using System.Linq;

namespace EventEaseBookingApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
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
                    (b.EventName != null && b.EventName.Contains(search)) ||
                    (b.VenueName != null && b.VenueName.Contains(search))
                );
            }

            return View(bookings.ToList());
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "VenueName");
            ViewBag.Events = new SelectList(_context.Events, "EventId", "EventName");
            return View();
        }

        // POST: Create (DOUBLE BOOKING VALIDATION)
        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            var exists = _context.Bookings.Any(b =>
                b.VenueId == booking.VenueId &&
                b.BookingDate == booking.BookingDate
            );

            if (exists)
            {
                ModelState.AddModelError("", "This venue is already booked for that date and time.");
            }

            if (ModelState.IsValid)
            {
                _context.Bookings.Add(booking);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "VenueName");
            ViewBag.Events = new SelectList(_context.Events, "EventId", "EventName");

            return View(booking);
        }

        // GET: Details ⭐ ADDED
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var booking = _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .FirstOrDefault(b => b.BookingId == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // GET: Edit ⭐ UPDATED
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking == null) return NotFound();

            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
            ViewBag.Events = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);

            return View(booking);
        }

        // POST: Edit ⭐ UPDATED (with validation)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Booking booking)
        {
            var exists = _context.Bookings.Any(b =>
                b.VenueId == booking.VenueId &&
                b.BookingDate == booking.BookingDate &&
                b.BookingId != booking.BookingId
            );

            if (exists)
            {
                ModelState.AddModelError("", "This venue is already booked for that date and time.");
            }

            if (ModelState.IsValid)
            {
                _context.Bookings.Update(booking);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "VenueName", booking.VenueId);
            ViewBag.Events = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);

            return View(booking);
        }

        // GET: Delete
        public IActionResult Delete(int? id)
        {
            var booking = _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .FirstOrDefault(b => b.BookingId == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == id);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}