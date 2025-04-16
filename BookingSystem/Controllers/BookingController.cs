using BookingSystem.Core.Dtos;
using BookingSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingInterface _bookingService;

        public BookingController(IBookingInterface bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserBookings()
        {
            var userId = User.FindFirst("UserId")?.Value;
            
            if (String.IsNullOrEmpty(userId))
                return BadRequest("No User ID found.");

            int id = int.Parse(userId);
            var bookings = await _bookingService.GetUserBookings(id);

            if (bookings == null)
                return NoContent();
            
            return Ok(bookings);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDto dto)
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (String.IsNullOrEmpty(userId))
                return BadRequest("No User ID found.");

            int id = int.Parse(userId);

            var booking = await _bookingService.CreateBooking(dto, id);

            if (!booking)
                return BadRequest();

            return Created();
        }
    }
}
