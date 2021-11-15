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
    public class HotelRepository : IHotelRepository
    {
        private readonly MainContext mainContext;

        public HotelRepository(MainContext mainContext)
        {
            this.mainContext = mainContext;
        }

        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            return await mainContext.Hotels.ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> SearchHotels(string name)
        {
            return await mainContext.Hotels.Where(x => x.Name.Contains(name)).ToListAsync();
        }

        public async Task<Hotel> GetHotel(int id)
        {
            return await mainContext.Hotels.Include(x => x.Bookings).SingleOrDefaultAsync(x => x.HotelId == id);
        }

        public async Task<IEnumerable<HotelRoom>> GetHotelRooms(int id)
        {
            return await mainContext.HotelRooms.Where(x => x.Hotel.HotelId == id).Include(x => x.RoomType).Include(x => x.Hotel).ToListAsync();
        }
    }
}
