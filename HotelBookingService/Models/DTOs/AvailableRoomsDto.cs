using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingLibrary.Models.DTOs
{
    public class AvailableRoomsDto
    {
        public int Number { get; set; }
        public string RoomTypeName { get; set; }
        public string HotelName { get; set; }
    }
}
