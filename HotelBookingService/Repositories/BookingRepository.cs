using HotelBookingLibrary.Context;
using HotelBookingLibrary.Models;
using HotelBookingLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingLibrary.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly MainContext mainContext;

        public BookingRepository(MainContext mainContext)
        {
            this.mainContext = mainContext;
        }

        public async Task<Booking> GetBooking(string bookingReference)
        {
            return await mainContext.Bookings.Include(x => x.Hotel).SingleOrDefaultAsync(x => x.BookingReference == bookingReference);
        }

        public async Task<IEnumerable<Booking>> GetBookings(int hotelId)
        {
            return await mainContext.Bookings.Where(x => x.Hotel.HotelId == hotelId).Include(x => x.Hotel).Include(x => x.RoomCalendars).ToListAsync();
        }

        public async Task CreateBooking(Booking booking)
        {
            await mainContext.Bookings.AddAsync(booking ?? throw new ArgumentNullException(nameof(booking)));
            await mainContext.RoomCalendars.AddRangeAsync(booking.RoomCalendars ?? throw new ArgumentNullException(nameof(booking)));
            await mainContext.SaveChangesAsync();
        }
    }
}
