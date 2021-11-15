using HotelBookingLibrary.Context;
using HotelBookingLibrary.Models.AutoMapperProfiles;
using HotelBookingLibrary.Repositories;
using HotelBookingLibrary.Repositories.Interfaces;
using HotelBookingLibrary.Services;
using HotelBookingLibrary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace HotelBookingLibrary
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddHotelBookingLibrary(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(HotelProfile));

            services.AddDbContext<MainContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IHotelService, HotelService>();

            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            return services;
        }
    }
}
