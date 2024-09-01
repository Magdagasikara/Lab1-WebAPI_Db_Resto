using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface IBookingServices
    {
        Task<IEnumerable<BookingWithTablesListVM>> GetAllBookingsAsync();
        Task<IEnumerable<BookingWithTablesListVM>> GetActiveBookingsAsync(DateTime dateTime);
        Task<BookingListVM> GetBookingByBookingNumberAsync(string bookingNr);
        Task<BookingWithTablesListVM> GetBookingWithTablesByBookingNumberAsync(string bookingNr);
        Task<IEnumerable<BookingListVM>> GetAllBookingsByEmailAsync(string email);
        Task<IEnumerable<BookingListVM>> GetActiveBookingsByEmailAsync(DateTime dateTime, string email);
        Task<IEnumerable<BookingWithTablesListVM>> GetBookingsByDateAsync(DateOnly date);
        Task AddBookingAsync(BookingDto booking);
        Task UpdateBookingAsync(string bookingNr, BookingDto updatedBooking);
        Task UpdateBookingTablesAsync(string bookingNr, BookingWithTablesDto updatedBookingWithTables);
        Task DeleteBookingByBookingNumberAsync(string bookingNr);
    }
}
