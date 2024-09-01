using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetActiveBookingsAsync(DateTime dateTime);
        Task<Booking> GetBookingByBookingNumberAsync(string bookingNr);
        Task<Booking> GetBookingWithTablesByBookingNumberAsync(string bookingNr);
        Task<IEnumerable<Booking>> GetAllBookingsByEmailAsync(string email);
        Task<IEnumerable<Booking>> GetActiveBookingsByEmailAsync(DateTime dateTime, string email);
        Task<IEnumerable<Booking>> GetBookingsByDateAsync(DateOnly date);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(string bookingNr, Booking updatedBooking);
        Task UpdateBookingTablesAsync(string bookingNr, Booking updatedBooking, List<Models.Table> tables);
        Task DeleteBookingByBookingNumberAsync(string bookingNr);
    }
}
