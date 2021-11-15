using AutoMapper;
using HotelBookingLibrary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingLibrary.Models.AutoMapperProfiles
{
    internal class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>();
        }
    }
}
