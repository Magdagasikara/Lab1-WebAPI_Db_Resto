using AutoMapper;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Table;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Lab1_WebAPI_Db_Resto.Services
{
    public class TableService : ITableServices
    {
        private readonly ITableRepository _tableRepo;
        private readonly IMapper _mapper;

        public TableService(ITableRepository tableRepo, IMapper mapper)
        {
            _tableRepo = tableRepo;
            _mapper = mapper;
        }
        public async Task AddTableAsync(TableDto table)
        {
            try
            {
                var newTable = _mapper.Map<Table>(table);
                await _tableRepo.AddTableAsync(newTable);
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding table in service", ex);
            }
        }

        public async Task DeleteTableByTableNrAsync(int tableNr)
        {
            try
            {
                await _tableRepo.DeleteTableByTableNrAsync(tableNr);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting table in service", ex);
            }
        }

        public async Task<IEnumerable<TableDto>> GetAllTablesAsync()
        {
            try
            {
                var tables = await _tableRepo.GetAllTablesAsync();

                return _mapper.Map<List<TableDto>>(tables);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting tables in service", ex);
            }
        }

        public async Task<IEnumerable<TableDto>> GetFreeTablesByTimeAsync(DateTime time, double reservationHours = 2)
        {
            try
            {
                var tables = await _tableRepo.GetFreeTablesByTimeAsync(time, reservationHours);

                return _mapper.Map<List<TableDto>>(tables);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting tables in service", ex);
            }
        }

        public async Task<TableDto> GetTableByTableNrAsync(int tableNr)
        {
            try
            {
                var table = await _tableRepo.GetTableByTableNrAsync(tableNr);
                return _mapper.Map<TableDto>(table);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting table in service", ex);
            }
        }

        public async Task UpdateTableAsync(TableUpdateDto table)
        {
            try
            {
                var updatedTable = _mapper.Map<Table>(table);
                if (table.UpdatedTableNumber is not null && table.UpdatedTableNumber != 0)
                {
                    updatedTable.TableNumber = table.UpdatedTableNumber.Value;
                }
                await _tableRepo.UpdateTableAsync(table.TableNumber, updatedTable);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating table in service", ex);
            }
        }
    }
}
