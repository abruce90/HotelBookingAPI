using AutoMapper;
using HotelBookingLibrary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingLibrary.Models.AutoMapperProfiles
{
    public class AvailableRoomsProfile : Profile
    {
        public AvailableRoomsProfile()
        {
            CreateMap<HotelRoom, AvailableRoomsDto>();
        }
    }
}
