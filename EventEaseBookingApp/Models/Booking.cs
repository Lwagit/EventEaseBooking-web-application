using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventEaseBookingApp.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Booking Date is required")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Venue is required")]
        public int VenueId { get; set; }

        [Required(ErrorMessage = "Event is required")]
        public int EventId { get; set; }

        // Navigation Properties
        [ForeignKey("VenueId")]
        [ValidateNever]
        public Venue? Venue { get; set; }

        [ForeignKey("EventId")]
        [ValidateNever]
        public Event? Event { get; set; }
    }
}