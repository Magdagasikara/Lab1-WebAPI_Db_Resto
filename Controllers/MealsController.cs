using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1_WebAPI_Db_Resto.Controllers
{
    [Route("api/menu")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IMealServices _mealServices;
        public MealsController(IMealServices mealServices)
        {
            _mealServices = mealServices;
        }

        [HttpPost("meal/add")]
        public async Task<ActionResult> AddMeal(MealDto meal)
        {
            try
            {
                await _mealServices.AddMealAsync(meal);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("meal/{mealId}/delete")]
        public async Task<ActionResult> DeleteMealById(int mealId)
        {
            try
            {
                await _mealServices.DeleteMealByIdAsync(mealId);
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

        [HttpGet("meals")]
        public async Task<ActionResult<IEnumerable<MealListVM>>> GetAllMeals()
        {
            try
            {
                return Ok(await _mealServices.GetAllMealsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("meal/{mealId}")] // ID shouldnt be exposed externally, I still need to find a good replacement
        public async Task<ActionResult<MealListVM>> GetMealById(int mealId)
        {
            try
            {
                return Ok(await _mealServices.GetMealByIdAsync(mealId));
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

        [HttpPatch("meal/update")]
        public async Task<ActionResult> UpdateMeal(MealUpdateDto meal)
        {
            try
            {
                await _mealServices.UpdateMealAsync(meal);
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
    }
}
