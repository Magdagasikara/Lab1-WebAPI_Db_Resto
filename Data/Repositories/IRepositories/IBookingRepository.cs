using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories
{
    public interface IBookingRepository
    {
        //Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetActiveBookingsAsync(DateTime dateTime);
        Task<Booking> GetBookingByBookingNumberAsync(int bookingNr);
        Task<Booking> GetBookingWithTablesByBookingNumberAsync(int bookingNr);
        Task<IEnumerable<Booking>> GetAllBookingsByEmailAsync(string email);
        Task<IEnumerable<Booking>> GetActiveBookingsByEmailAsync(DateTime dateTime, string email);
        Task<IEnumerable<Booking>> GetBookingsByDateAsync(DateOnly date);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(int bookingNr, Booking updatedBooking);
        Task UpdateBookingTablesAsync(int bookingNr, Booking updatedBooking);
        Task DeleteBookingByBookingNumberAsync(int bookingNr);
    }
}
