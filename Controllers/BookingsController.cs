using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1_WebAPI_Db_Resto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        public readonly IBookingServices _bookingServices;
        public BookingsController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        [HttpPost("AddBooking")]
        public async Task<ActionResult> AddBooking(BookingDto booking)
        {
            try
            {
                await _bookingServices.AddBookingAsync(booking);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<BookingListVM>> GetAllBookings()
        {
            try
            {
                var x = await _bookingServices.GetAllBookingsAsync();
                return Ok(x);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
