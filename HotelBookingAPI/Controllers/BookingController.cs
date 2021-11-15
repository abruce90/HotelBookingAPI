using HotelBookingLibrary.Models.DTOs;
using HotelBookingLibrary.Repositories.Interfaces;
using HotelBookingLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpGet("{bookingRef}")]
        public async Task<IActionResult> GetByReference(string bookingRef)
        {
            return Ok(await bookingService.SearchBooking(bookingRef));
        }

        [HttpPost("CheckAvailability")]
        public async Task<IActionResult> CheckAvailability([FromBody] BookingRequestDto request)
        {
            return Ok(await bookingService.CheckRoomAvailability(request));
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto request)
        {
            var bookingRef = await bookingService.CreateBooking(request);
            if (bookingRef == null)
                return BadRequest();

            return Accepted(bookingRef);
        }

    }
}
