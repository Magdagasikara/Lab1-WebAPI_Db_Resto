using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly RestoContext _context;
        public TableRepository(RestoContext context)
        {
            _context = context;
        }

        public async Task AddTableAsync(Table table)
        {
            try
            {
                await _context.Tables.AddAsync(table);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when adding a new table", ex);
            }
        }

        public async Task DeleteTableByTableNrAsync(int tableNr)
        {
            try
            {
                var table = await GetTableByTableNrAsync(tableNr);
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when deleting a table", ex);
            }
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            try
            {
                return await _context.Tables.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of tables", ex);
            }
        }

        public async Task<IEnumerable<Table>> GetFreeTablesByTimeAsync(DateTime time, double reservationHours = 2)
        {
            try
            {
                return await _context.Tables
                            .Where(t => !t.TableBooking.Any(tb => (
                                // reservation started earlier and continues during the time we check for
                                (tb.Booking.ReservationStart <= time &&
                                tb.Booking.ReservationEnd > time
                                ) ||
                                // reservation starts during the time we check for
                                (tb.Booking.ReservationStart > time &&
                                tb.Booking.ReservationStart < time.AddHours(reservationHours)
                                ))
                                ))
                            .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of tables", ex);
            }
        }

        public async Task<Table> GetTableByTableNrAsync(int tableNr)
        {
            try
            {
                var table = await _context.Tables
                    .SingleOrDefaultAsync(c => c.TableNumber == tableNr);
                if (table != null)
                {
                    return table;
                }
                throw new KeyNotFoundException($"No table with {tableNr}");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when getting a table", ex);
            }
        }

        public async Task UpdateTableAsync(int tableNr, Table updatedTable)
        {
            if (updatedTable == null || tableNr == 0)
            {
                throw new ArgumentNullException(nameof(updatedTable), "No table to be updated");
            }
            try
            {
                var table = await GetTableByTableNrAsync(tableNr);
                table.AmountOfPlaces = updatedTable.AmountOfPlaces;
                table.TableNumber = updatedTable.TableNumber;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Table to be updated does not exist in Db");
            }
            catch (Exception ex)
            {
                throw new Exception("Error when updating a table", ex);
            }
        }
    }
}
