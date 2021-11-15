using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelBookingLibrary.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int MaxOccupancy { get; set; }
    }
}
