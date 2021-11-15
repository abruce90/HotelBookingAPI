using HotelBookingLibrary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingLibrary.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> SearchBooking(string bookingReference);

        Task<IEnumerable<AvailableRoomsDto>> CheckRoomAvailability(BookingRequestDto bookingRequest);

        Task<BookingDto> CreateBooking(BookingRequestDto bookingRequest);
    }
}
