using Lab1_WebAPI_Db_Resto.Models;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<IEnumerable<Table>> GetFreeTablesByTimeAsync(DateTime time, double reservationHours = 2);
        Task<Table> GetTableByTableNrAsync(int tableNr);
        Task AddTableAsync(Table table);
        Task UpdateTableAsync(int tableNr, Table table);
        Task DeleteTableByTableNrAsync(int tableNr);
    }
}
