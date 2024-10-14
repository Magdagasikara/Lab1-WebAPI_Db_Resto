using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Services;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<IEnumerable<Table>> GetFreeTablesByTimeAsync(DateTime time, double reservationHours = 2);
        Task<IEnumerable<Table>> GetFreeTablesWithBookingOriginalByTimeAsync(DateTime time, double reservationHours, Booking bookingOriginal);
        Task<IEnumerable<Table>> AssignTablesToBookingAsync(Booking booking, Booking? bookingOriginal = null);
        Task BookTablesAsync(Booking booking, IEnumerable<Table> tables);
        Task<Table> GetTableByTableNrAsync(int tableNr);
        Task AddTableAsync(Table table);
        Task UpdateTablesAsync(Booking booking, IEnumerable<Table> tables);
        Task DeleteTableByTableNrAsync(int tableNr);
    }
}
