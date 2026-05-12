using Microsoft.AspNetCore.Mvc;
using EventEaseBookingApp.Models;
using EventEaseBookingApp.Data;
using System.Linq;

namespace EventEaseBookingApp.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Venues.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var venue = _context.Venues.FirstOrDefault(v => v.VenueId == id);
            if (venue == null) return NotFound();

            return View(venue);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Venues.Add(venue);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venue);
        }

        // 🔵 EDIT GET
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var venue = _context.Venues.FirstOrDefault(v => v.VenueId == id);
            if (venue == null) return NotFound();

            return View(venue);
        }

        // 🔵 EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Venue venue)
        {
            if (id != venue.VenueId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Venues.Update(venue);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(venue);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var venue = _context.Venues.FirstOrDefault(v => v.VenueId == id);
            return View(venue);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var venue = _context.Venues.FirstOrDefault(v => v.VenueId == id);

            if (_context.Bookings.Any(b => b.VenueId == id))
            {
                ViewBag.Error = "Cannot delete this venue because it has active bookings.";
                return View("Delete", venue);
            }

            if (venue != null)
            {
                _context.Venues.Remove(venue);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}