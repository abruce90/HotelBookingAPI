using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelBookingLibrary.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        [Required]
        public string BookingReference { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }
        [Required]
        public string ReservationName { get; set; }

        public Hotel Hotel { get; set; }

        public ICollection<RoomCalendar> RoomCalendars { get; set; }
    }
}

