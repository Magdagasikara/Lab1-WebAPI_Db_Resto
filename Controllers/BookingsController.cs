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
                return Created();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<BookingListVM>> GetAllBookings()
        {
            try
            {
                return Ok(await _bookingServices.GetAllBookingsAsync());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("DeleteBooking/{bookingNumber}")]
        public async Task<ActionResult> DeleteBooking(string bookingNumber)
        {
            try
            {
                await _bookingServices.DeleteBookingByBookingNumberAsync(bookingNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

    }
}
