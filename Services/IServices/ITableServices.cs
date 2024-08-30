using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface ITableServices
    {
        Task<IEnumerable<TableListVM>> GetAllTablesAsync();
        Task<TableListVM> GetTableByTableNrAsync(int tableNr);
        Task<IEnumerable<TableListVM>> GetFreeTablesByTimeAsync(DateTime time, double reservationHours = 2);
        Task AddTableAsync(TableDto table);
        Task UpdateTableAsync(TableUpdateDto table);
        Task DeleteTableByTableNrAsync(int tableNr);
    }
}
