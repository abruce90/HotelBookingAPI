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
           // var hotel = await hotelRepository.GetHotel(bookingRequest.HotelId);

            var hotelRooms = await hotelRepository.GetHotelRooms(bookingRequest.HotelId);

            var bookings = await bookingRepository.GetBookings(bookingRequest.HotelId);

            //get existing overlapping bookings for hotel 
            var existingBookings = bookings?.Where(x => (x.StartDate >= bookingRequest.DateFrom 
                                                                && x.StartDate <= bookingRequest.DateTo)
                                                            || (x.EndDate >= bookingRequest.DateFrom 
                                                                && x.EndDate <= bookingRequest.DateTo)).SelectMany(x => x.RoomCalendars).ToList();

            if (existingBookings != null && existingBookings.Any())
            {
                var bookedRoomIds = existingBookings?.Select(x => x.HotelRoom.HotelRoomId).ToList();

                //determine if remaining capacity can accomodate the booking party
                var remainingRooms = hotelRooms.Where(x => !bookedRoomIds.Contains(x.HotelRoomId)).ToList();

                if (remainingRooms.Where(x => (bookingRequest.RoomTypeId == null || bookingRequest.RoomTypeId == 0) || x.RoomType.RoomTypeId == bookingRequest.RoomTypeId).Select(x => x.RoomType.MaxOccupancy).Sum() >= bookingRequest.NumberOfGuests)
                {
                    //return available rooms to book
                    return mapper.Map<IEnumerable<HotelRoom>, IEnumerable<AvailableRoomsDto>>(remainingRooms.Where(x => (bookingRequest.RoomTypeId == null || bookingRequest.RoomTypeId == 0) || x.RoomType.RoomTypeId == bookingRequest.RoomTypeId).ToList());
                }
            }
            else
                return mapper.Map<IEnumerable<HotelRoom>, IEnumerable<AvailableRoomsDto>>(hotelRooms.Where(x => (bookingRequest.RoomTypeId == null || bookingRequest.RoomTypeId == 0) || x.RoomType.RoomTypeId == bookingRequest.RoomTypeId).ToList());

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
            booking.RoomCalendars = await ReserveRooms(bookingRequest, booking);

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
                ReservationName = bookingRequest.ReservationName,
                BookingReference = $"{hotel.ShortCode}{bookingRequest.DateFrom.ToString("yyyyMMdd")}{hotel.Bookings.Count}",
                Hotel = hotel
            };
            return booking;
        }

        private async Task<List<RoomCalendar>> ReserveRooms(BookingRequestDto bookingRequest, Booking booking)
        {
            var hotelRooms = await hotelRepository.GetHotelRooms(bookingRequest.HotelId);

            if(bookingRequest.RoomTypeId != null && bookingRequest.RoomTypeId != 0)
                hotelRooms = hotelRooms.Where(x => x.RoomType.RoomTypeId == bookingRequest.RoomTypeId).ToList();

            List<RoomCalendar> reservedRooms = new List<RoomCalendar>();

            int i = 0;

            while (i < bookingRequest.NumberOfGuests)
            {
                reservedRooms.Add(new RoomCalendar()
                {
                    Booking = booking,
                    HotelRoom = bookingRequest.NumberOfGuests > 1 ? hotelRooms.OrderByDescending(x => x.RoomType.MaxOccupancy).First() : hotelRooms.OrderBy(x => x.RoomType.MaxOccupancy).First()
                });

                i = reservedRooms.Select(x => x.HotelRoom.RoomType.MaxOccupancy).Sum();
            }

            return reservedRooms;
        }
    }
}
