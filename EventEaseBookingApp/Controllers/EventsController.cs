using Microsoft.AspNetCore.Mvc;
using EventEaseBookingApp.Models;
using EventEaseBookingApp.Data;
using System.Linq;

namespace EventEaseBookingApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Events.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Event e)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Add(e);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(e);
        }

        // 🔵 DETAILS (ADDED - FIXES YOUR 404 ERROR)
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var ev = _context.Events.FirstOrDefault(e => e.EventId == id);
            if (ev == null) return NotFound();

            return View(ev);
        }

        // 🔵 EDIT GET
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var ev = _context.Events.FirstOrDefault(e => e.EventId == id);
            if (ev == null) return NotFound();

            return View(ev);
        }

        // 🔵 EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Event e)
        {
            if (id != e.EventId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Events.Update(e);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(e);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var ev = _context.Events.FirstOrDefault(e => e.EventId == id);
            return View(ev);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var ev = _context.Events.FirstOrDefault(e => e.EventId == id);

            if (_context.Bookings.Any(b => b.EventId == id))
            {
                ViewBag.Error = "Cannot delete this event because it has active bookings.";
                return View("Delete", ev);
            }

            if (ev != null)
            {
                _context.Events.Remove(ev);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}