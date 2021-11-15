using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingLibrary.Models.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public string BookingReference { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string HotelName { get; set; }
    }
}
