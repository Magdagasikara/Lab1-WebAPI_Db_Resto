using AutoMapper;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Meal;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services.IServices;

namespace Lab1_WebAPI_Db_Resto.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepo;
        private readonly IMapper _mapper;

        public MealService(IMealRepository mealRepo, IMapper mapper)
        {
            _mealRepo = mealRepo;
            _mapper = mapper;
        }
        public async Task AddMealAsync(MealWithCategoryDto meal)
        {
            try
            {
                var newMeal = _mapper.Map<Meal>(meal);
                // !!!! Add check if category exists
                await _mealRepo.AddMealAsync(newMeal);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding meal in service", ex);
            }
        }

        public async Task DeleteMealByIdAsync(int mealId)
        {
            try
            {
                await _mealRepo.DeleteMealByIdAsync(mealId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting meal in service", ex);
            }
        }

        public async Task<IEnumerable<MealWithCategoryDto>> GetAllMealsAsync()
        {
            try
            {
                var meals = await _mealRepo.GetAllMealsAsync();
                var mealDtos = new List<MealWithCategoryDto>();
                foreach (var meal in meals)
                {
                    var mealDto = _mapper.Map<MealWithCategoryDto>(meal);
                    if (meal.MealCategory is not null)
                    {
                        mealDto.FK_MealCategoryId = meal.MealCategory.Id;
                    }
                    mealDtos.Add(mealDto);
                }
                return mealDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting meals in service", ex);
            }
        }

        public async Task<MealWithCategoryDto> GetMealByIdAsync(int mealId)
        {
            try
            {
                var meal = await _mealRepo.GetMealByIdAsync(mealId);
                return _mapper.Map<MealWithCategoryDto>(meal);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting meal in service", ex);
            }
        }

        public async Task UpdateMealAsync(MealUpdateDto meal)
        {
            try
            {
                var updatedMeal = _mapper.Map<Meal>(meal);
                // !!!! Add check if category exists
                await _mealRepo.UpdateMealAsync(updatedMeal);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating meal in service", ex);
            }
        }
    }
}
