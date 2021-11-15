using HotelBookingLibrary.Models;
using System.Threading.Tasks;

namespace HotelBookingLibrary.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateBooking(Booking booking);
        Task<Booking> GetBooking(string bookingReference);
    }
}