﻿using Lab1_WebAPI_Db_Resto.Models.DTOs.Customer;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Table;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1_WebAPI_Db_Resto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableServices _tableServices;
        public TablesController(ITableServices tableServices)
        {
            _tableServices = tableServices;
        }

        [HttpPost("table/add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddTable(TableDto table)
        {
            try
            {
                await _tableServices.AddTableAsync(table);
                return Created();
            }
            //catch (DbUpdateException){}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("table/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTableByTableNr(TableNumberDto table)
        {
            try
            {
                await _tableServices.DeleteTableByTableNrAsync(table.TableNumber);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // default route
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetAllTables()
        {
            try
            {
                return Ok(await _tableServices.GetAllTablesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("available")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetFreeTablesByTime(DateTime time, double reservationHours = 2)
        {
            try
            {
                return Ok(await _tableServices.GetFreeTablesByTimeAsync(time, reservationHours));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("places/available")]
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("table")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CustomerDto>> GetTableByTableNr(TableNumberDto table)
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
                return BadRequest(ex.Message);
            }
        }



        //[HttpPatch("table/update")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> UpdateTable(TableUpdateDto table)
        //{
        //    try
        //    {
        //        await _tableServices.UpdateTableAsync(table);
        //        return NoContent();
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
