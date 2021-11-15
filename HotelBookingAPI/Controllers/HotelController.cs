using HotelBookingLibrary.Models;
using HotelBookingLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> logger;
        private readonly IHotelService hotelService;

        public HotelController(ILogger<HotelController> logger, IHotelService hotelService)
        {
            this.logger = logger;
            this.hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await hotelService.GetHotels());
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok(await hotelService.SearchHotels(name));
        }
    }
}
