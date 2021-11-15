using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelBookingLibrary.Models
{
    public class HotelRoom
    {
        public int HotelRoomId { get; set; }
        [Required]
        public int Number { get; set; }
       
        public RoomType RoomType { get; set; }
        public Hotel Hotel { get; set; }

        public ICollection<RoomCalendar> RoomCalendars { get; set; }
    }
}
