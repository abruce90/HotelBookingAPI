using AutoMapper;
using HotelBookingLibrary.Models;
using HotelBookingLibrary.Models.DTOs;
using HotelBookingLibrary.Repositories.Interfaces;
using HotelBookingLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingLibrary.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public BookingService(IBookingRepository bookingRepository, IHotelRepository hotelRepository, IMapper mapper)
        {
            this.bookingRepository = bookingRepository;
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task<BookingDto> SearchBooking(string bookingReference)
        {
            return mapper.Map<Booking,BookingDto>(await bookingRepository.GetBooking(bookingReference));
        }

        public async Task<IEnumerable<AvailableRoomsDto>> CheckRoomAvailability(BookingRequestDto bookingRequest)
        {
            //get hotel
            var hotel = await hotelRepository.GetHotel(bookingRequest.HotelId);

            //get existing overlapping bookings for hotel 
            var existingBookings = hotel.Bookings.Where(x => (x.StartDate >= bookingRequest.DateFrom 
                                                                && x.StartDate <= bookingRequest.DateTo)
                                                            || (x.EndDate >= bookingRequest.DateFrom 
                                                                && x.EndDate <= bookingRequest.DateTo)).SelectMany(x => x.RoomCalendars).ToList();

            var bookedRooms = existingBookings.Select(x => x.HotelRoom.HotelRoomId).ToList();

            //determine if remaining capacity can accomodate the booking party
            var remainingRooms = hotel.HotelRooms.Where(x => !bookedRooms.Contains(x.HotelRoomId)).ToList();
            
            if(remainingRooms.Select(x => x.RoomType.MaxOccupancy).Sum() >= bookingRequest.NumberOfGuests)
            {
                //return available rooms to book
                return mapper.Map<IEnumerable<HotelRoom>,IEnumerable<AvailableRoomsDto>>(remainingRooms.ToList());
            }

            return null;
        }

        public async Task<BookingDto> CreateBooking(BookingRequestDto bookingRequest)
        {
            //Check availability
            var availbility = await CheckRoomAvailability(bookingRequest);

            if (availbility == null || !availbility.Any())
                return null;

            //Build booking request
            var booking = await BuildBooking(bookingRequest);

            //create booking
            await bookingRepository.CreateBooking(booking);

            //return booking reference
            return mapper.Map<Booking,BookingDto>(booking);
        }

        private async Task<Booking> BuildBooking(BookingRequestDto bookingRequest)
        {
            var hotel = await hotelRepository.GetHotel(bookingRequest.HotelId);

            Booking booking = new Booking()
            {
                StartDate = bookingRequest.DateFrom,
                EndDate = bookingRequest.DateTo,
                NumberOfGuests = bookingRequest.NumberOfGuests,
                BookingReference = $"{hotel.ShortCode}{bookingRequest.DateFrom.ToString("yyyyMMdd")}",
                Hotel = hotel
            };
            return booking;
        }
    }
}
