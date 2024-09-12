using AutoMapper;
using Lab1_WebAPI_Db_Resto.Data.Repositories;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.MealCategory;
using Lab1_WebAPI_Db_Resto.Services.IServices;

namespace Lab1_WebAPI_Db_Resto.Services
{
    public class MealCategoryService : IMealCategoryService
    {
        private readonly IMealCategoryRepository _mealCategoryRepo;
        private readonly IMapper _mapper;
        public MealCategoryService(IMealCategoryRepository mealCategoryRepo, IMapper mapper)
        {
            _mealCategoryRepo = mealCategoryRepo;
            _mapper = mapper;
        }
        public async Task AddMealCategoryAsync(MealCategoryDto category)
        {
            try
            {
                var newMealCategory = _mapper.Map<MealCategory>(category);
                await _mealCategoryRepo.AddMealCategoryAsync(newMealCategory);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding meal category in service", ex);
            }
        }

        public async Task DeleteMealCategoryByIdAsync(int categoryId)
        {
            try
            {
                await _mealCategoryRepo.DeleteMealCategoryByIdAsync(categoryId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<MealCategoryDto>> GetAllMealCategoriesAsync()
        {
            try
            {
                var mealCategories = await _mealCategoryRepo.GetAllMealCategoriesAsync();

                return _mapper.Map<List<MealCategoryDto>>(mealCategories);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting meal categories in service", ex);
            }
        }

        public async Task<MealCategoryWithMealsDto> GetMealCategoryByIdAsync(int categoryId)
        {
            try
            {
                var mealCategory = await _mealCategoryRepo.GetMealCategoryByIdAsync(categoryId);
                return _mapper.Map<MealCategoryWithMealsDto>(mealCategory);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting meal category in service", ex);
            }
        }

        public async Task UpdateMealCategoryAsync(MealCategoryUpdateDto category)
        {
            try
            {
                var updatedMealCategory = _mapper.Map<MealCategory>(category);
                await _mealCategoryRepo.UpdateMealCategoryAsync(updatedMealCategory);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating meal category in service", ex);
            }
        }
    }
}
