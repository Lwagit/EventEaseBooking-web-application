using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EventEaseBookingApp.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }


    [Required]
        [Display(Name = "Venue Name")]
        public string VenueName { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public int Capacity { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        // NEW AVAILABILITY FIELD
        [Display(Name = "Venue Available")]
        public bool IsAvailable { get; set; } = true;

        // Navigation Property
        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }


}
