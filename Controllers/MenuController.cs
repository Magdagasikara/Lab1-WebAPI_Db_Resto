using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Meal;
using Lab1_WebAPI_Db_Resto.Models.DTOs.MealCategory;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1_WebAPI_Db_Resto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMealService _mealServices;
        private readonly IMealCategoryService _mealCategoryServices;
        public MenuController(IMealService mealServices, IMealCategoryService mealCategoryServices)
        {
            _mealServices = mealServices;
            _mealCategoryServices = mealCategoryServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealCategoryWithMealsDto>>> GetCurrentMenu()
        {
            try
            {
                var menu = await _mealCategoryServices.GetAllMealCategoriesWithMealsAsync();
            
                var menuCurrent = menu.Select(mc => new MealCategoryWithMealsDto
                {
                    Name = mc.Name,
                    Meals = mc.Meals?.Where(m => m.IsAvailable).ToList()
                });
                return Ok(menuCurrent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("meals/meal/add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddMeal(MealWithCategoryDto meal)
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

        [HttpDelete("meals/meal/{mealId}/delete")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<MealWithCategoryDto>>> GetAllMeals()
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

        [HttpGet("meals/meal/{mealId}")] // ID shouldnt be exposed externally, I still need to find a good replacement
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MealWithCategoryDto>> GetMealById(int mealId)
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

        [HttpPatch("meals/meal/update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateMeal(MealUpdateDto meal)
        {
            Console.WriteLine("halo!!");
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

        [HttpPost("categories/category/add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddMealCategory(MealCategoryDto mealCategory)
        {
            try
            {
                await _mealCategoryServices.AddMealCategoryAsync(mealCategory);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("categories/category/{categoryId}/delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteMealCategoryById(int categoryId)
        {
            try
            {
                await _mealCategoryServices.DeleteMealCategoryByIdAsync(categoryId);
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

        [HttpGet("categories")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<MealCategoryDto>>> GetAllMealCategories()
        {
            try
            {
                return Ok(await _mealCategoryServices.GetAllMealCategoriesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("categories/category/{categoryId}")] // ID shouldnt be exposed externally, I still need to find a good replacement
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MealCategoryWithMealsDto>> GetMealCategoryById(int categoryId)
        {
            try
            {
                return Ok(await _mealCategoryServices.GetMealCategoryByIdAsync(categoryId));
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

        [HttpPatch("categories/category/update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateMealCategory(MealCategoryUpdateDto mealCategory)
        {
            try
            {
                await _mealCategoryServices.UpdateMealCategoryAsync(mealCategory);
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
