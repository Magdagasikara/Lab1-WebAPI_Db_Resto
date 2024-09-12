using Lab1_WebAPI_Db_Resto.Models.DTOs.Table;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface ITableServices
    {
        Task<IEnumerable<TableDto>> GetAllTablesAsync();
        Task<TableDto> GetTableByTableNrAsync(int tableNr);
        Task<IEnumerable<TableDto>> GetFreeTablesByTimeAsync(DateTime time, double reservationHours = 2);
        Task AddTableAsync(TableDto table);
        Task UpdateTableAsync(TableUpdateDto table);
        Task DeleteTableByTableNrAsync(int tableNr);
    }
}
