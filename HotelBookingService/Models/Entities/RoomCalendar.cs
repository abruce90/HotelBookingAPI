using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelBookingLibrary.Models
{
    public class RoomCalendar
    {
        public int RoomCalendarId { get; set; }
      
        public Booking Booking { get; set; }
        public HotelRoom HotelRoom { get; set; }

    }
}
