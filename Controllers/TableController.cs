using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Lab1_WebAPI_Db_Resto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableServices _tableServices;
        public TableController(ITableServices tableServices)
        {
            _tableServices = tableServices;
        }

        [HttpPost("AddTable")]
        public async Task<ActionResult> AddTable(TableDto table)
        {
            try
            {
                await _tableServices.AddTableAsync(table);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete("DeleteTable")]
        public async Task<ActionResult> DeleteTableByTableNr(TableNumberDto table)
        {
            try
            {
                await _tableServices.DeleteTableByTableNrAsync(table.TableNumber);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("GetAllTables")]
        public async Task<ActionResult<IEnumerable<TableListVM>>> GetAllTables()
        {
            try
            {
                return Ok(await _tableServices.GetAllTablesAsync());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("GetFreeTables")]
        public async Task<ActionResult<IEnumerable<TableListVM>>> GetFreeTablesByTime(DateTime time, double reservationHours = 2)
        {
            try
            {
                return Ok(await _tableServices.GetFreeTablesByTimeAsync(time, reservationHours));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("GetFreePlaces")]
        public async Task<ActionResult<int>> GetFreePlacesByTime(DateTime time, double reservationHours = 2)
        {
            try
            {

                var freeTables = await _tableServices
                    .GetFreeTablesByTimeAsync(time, reservationHours);
                var freePlaces = freeTables
                    .Sum(s =>s.AmountOfPlaces);
                return Ok(freePlaces);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("GetTableByTableNr")]
        public async Task<ActionResult<CustomerListVM>> GetTableByTableNr(TableNumberDto table)
        {
            try
            {
                return Ok(await _tableServices.GetTableByTableNrAsync(table.TableNumber));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [HttpPatch("UpdateTable")]
        public async Task<ActionResult> UpdateTable(TableUpdateDto table)
        {
            try
            {
                await _tableServices.UpdateTableAsync(table);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
