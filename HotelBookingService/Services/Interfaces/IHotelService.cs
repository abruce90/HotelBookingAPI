using HotelBookingLibrary.Models;
using HotelBookingLibrary.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingLibrary.Services.Interfaces
{
    public interface IHotelService
    {
        Task<HotelDto> GetHotel(int hotelId);
        Task<IEnumerable<HotelDto>> GetHotels();
        Task<IEnumerable<HotelDto>> SearchHotels(string hotelName);
    }
}