using HotelBookingLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingLibrary.Repositories.Interfaces
{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotel(int id);
        Task<IEnumerable<Hotel>> GetHotels();
        Task<IEnumerable<Hotel>> SearchHotels(string name);
    }
}