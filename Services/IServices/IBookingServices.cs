using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface IBookingServices
    {
        Task<IEnumerable<BookingWithTablesListVM>> GetActiveBookingsAsync(DateTime dateTime);
        Task<BookingListVM> GetBookingByBookingNumberAsync(int bookingNr);
        Task<BookingWithTablesListVM> GetBookingWithTablesByBookingNumberAsync(int bookingNr);
        Task<IEnumerable<BookingListVM>> GetAllBookingsByEmailAsync(string email);
        Task<IEnumerable<BookingListVM>> GetActiveBookingsByEmailAsync(DateTime dateTime, string email);
        Task<IEnumerable<BookingWithTablesListVM>> GetBookingsByDateAsync(DateOnly date);
        Task AddBookingAsync(BookingDto booking);
        Task UpdateBookingAsync(int bookingNr, BookingDto updatedBooking);
        Task UpdateBookingTablesAsync(int bookingNr, BookingWithTablesDto updatedBookingWithTables);
        Task DeleteBookingByBookingNumberAsync(int bookingNr);
    }
}
