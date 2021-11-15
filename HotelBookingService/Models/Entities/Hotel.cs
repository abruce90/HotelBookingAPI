using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelBookingLibrary.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortCode { get; set; }
        public string Address { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public ICollection<HotelRoom> HotelRooms { get; set; }
    }
}
