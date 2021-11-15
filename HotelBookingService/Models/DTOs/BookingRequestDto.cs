using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingLibrary.Models.DTOs
{
    public class BookingRequestDto
    {
        public int HotelId { get; set; }
        public int NumberOfGuests { get; set; }
        public string ReservationName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? RoomTypeId { get; set; }
    }
}
