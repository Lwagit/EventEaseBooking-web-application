using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventEaseBookingApp.Models
{
    public class EventType
    {
        [Key]
        public int EventTypeId { get; set; }

    [Required]
        [Display(Name = "Event Type")]
        public string EventTypeName { get; set; } = string.Empty;

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }


}

