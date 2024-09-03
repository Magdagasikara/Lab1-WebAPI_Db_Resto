using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<Table>> BookAndGetTablesByTimeAsync(Booking booking)
        {
            try
            {
                int amountOfGuests = booking.AmountOfGuests;

                // get a list of all free tables
                var freeTables = await GetFreeTablesByTimeAsync(
                        booking.ReservationStart,
                        (booking.ReservationEnd - booking.ReservationStart).TotalHours
                        );

                // in first place, try to book a table with this exact amount of places
                var exactMatch = freeTables
                    .FirstOrDefault(ft => ft.AmountOfPlaces == amountOfGuests);
                if (exactMatch is not null)
                {
                    var bookedTables = new List<Table> { exactMatch };
                    await BookTablesAsync(booking, bookedTables);
                    return bookedTables;
                }


                // in second place, try too book a table with slightly higher amount of places (no more than 3 extra)
                var secondMatch = freeTables
                    .Where(ft => ft.AmountOfPlaces > amountOfGuests && ft.AmountOfPlaces <= amountOfGuests + 3)
                    .OrderByDescending(ft => ft.AmountOfPlaces)
                    .FirstOrDefault();
                if (secondMatch is not null)
                {
                    var bookedTables = new List<Table> { secondMatch };
                    await BookTablesAsync(booking, bookedTables);
                    return bookedTables;
                }

                // in third place, try to book combination of tables with correct amount of places or a bigger table
                var thirdMatch = new List<Table>();
                int prebookedPlaces = 0;
                foreach (var table in freeTables)
                {
                    prebookedPlaces += table.AmountOfPlaces;
                    thirdMatch.Add(table);
                    if (prebookedPlaces >= amountOfGuests)
                    {
                        var bookedTables = thirdMatch;
                        await BookTablesAsync(booking, bookedTables);
                        return bookedTables;
                    }
                    //break;
                }

                // otherwise if there were not enough places return an empty list
                throw new Exception ("Not enough places for this booking");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task BookTablesAsync(Booking booking, List<Table> tables)
        {
            try
            {
                if (tables.Count == 0)
                {
                    throw new Exception("Not enough tables for this reservation.");
                }
                foreach (var table in tables)
                {
                    var tableBooking = new TableBooking { Booking = booking, Table = table/*, FK_BookingId=booking.Id, FK_TableId=table.Id */};
                    await _context.TableBookings.AddAsync(tableBooking);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception){
                throw;
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
