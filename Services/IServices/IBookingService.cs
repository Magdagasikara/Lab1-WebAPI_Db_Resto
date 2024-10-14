using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Booking;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingWithTablesEndTimeDto>> GetAllBookingsAsync();
        //Task<IEnumerable<BookingWithTablesEndTimeDto>> GetActiveBookingsAsync(DateTime dateTime);
        Task<BookingListVM> GetBookingByBookingNumberAsync(string bookingNr);
        Task<BookingWithTablesEndTimeDto> GetBookingWithTablesByBookingNumberAsync(string bookingNr);
        Task<IEnumerable<BookingListVM>> GetAllBookingsByEmailAsync(string email);
        Task<IEnumerable<BookingListVM>> GetActiveBookingsByEmailAsync(DateTime dateTime, string email);
        //Task<IEnumerable<BookingWithTablesEndTimeDto>> GetBookingsByDateAsync(DateOnly date);
        Task AddBookingAsync(BookingDto booking);
        Task UpdateBookingAsync(BookingUpdateDto booking);
        Task UpdateBookingTablesAsync(string bookingNr, BookingWithTablesDto updatedBookingWithTables);
        Task DeleteBookingByBookingNumberAsync(string bookingNr);
    }
}
