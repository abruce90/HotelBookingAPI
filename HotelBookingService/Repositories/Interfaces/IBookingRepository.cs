using HotelBookingLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingLibrary.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateBooking(Booking booking);
        Task<Booking> GetBooking(string bookingReference);
        Task<IEnumerable<Booking>> GetBookings(int hotelId);
    }
}