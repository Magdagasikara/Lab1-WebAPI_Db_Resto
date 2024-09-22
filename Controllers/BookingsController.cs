using Lab1_WebAPI_Db_Resto.Models.DTOs.Booking;
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
        public readonly IBookingService _bookingServices;
        public BookingsController(IBookingService bookingServices)
        {
            _bookingServices = bookingServices;
        }

        [HttpPost("booking/add")]
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
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("booking/{bookingNumber}/delete")]
        public async Task<ActionResult> DeleteBooking(string bookingNumber)
        {
            try
            {
                await _bookingServices.DeleteBookingByBookingNumberAsync(bookingNumber);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // default route
        [HttpGet]
        public async Task<ActionResult<BookingWithTablesEndTimeDto>> GetAllBookings()
        {
            try
            {
                return Ok(await _bookingServices.GetAllBookingsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("booking/{bookingNumber}/simple")]
        public async Task<ActionResult<BookingListVM>> GetBookingByBookingNumber(string bookingNumber)
        {
            try
            {
                return Ok(await _bookingServices.GetBookingByBookingNumberAsync(bookingNumber));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("booking/{bookingNumber}/detailed")]
        public async Task<ActionResult<BookingWithTablesEndTimeDto>> GetBookingWithTablesByBookingNumber(string bookingNumber)
        {
            try
            {
                return Ok(await _bookingServices.GetBookingWithTablesByBookingNumberAsync(bookingNumber));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
