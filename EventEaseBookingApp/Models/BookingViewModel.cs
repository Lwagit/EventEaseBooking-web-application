using System;

namespace EventEaseBookingApp.Models
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string VenueName { get; set; }
        public string EventName { get; set; }
    }
}