using AutoMapper;
using HotelBookingLibrary.Models;
using HotelBookingLibrary.Models.DTOs;
using HotelBookingLibrary.Repositories.Interfaces;
using HotelBookingLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingLibrary.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task<HotelDto> GetHotel(int hotelId)
        {
            return  mapper.Map<HotelDto>(await hotelRepository.GetHotel(hotelId));
        }

        public async Task<IEnumerable<HotelDto>> GetHotels()
        {
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDto>>(await hotelRepository.GetHotels());
        }

        public async Task<IEnumerable<HotelDto>> SearchHotels(string hotelName)
        {
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDto>>(await hotelRepository.SearchHotels(hotelName));
        }
    }
}
