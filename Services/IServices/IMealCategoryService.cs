using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.MealCategory;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface IMealCategoryService
    {
        Task<IEnumerable<MealCategoryDto>> GetAllMealCategoriesAsync();
        Task<IEnumerable<MealCategoryWithMealsDto>> GetAllMealCategoriesWithMealsAsync();
        Task<MealCategoryWithMealsDto> GetMealCategoryByIdAsync(int categoryId);
        Task AddMealCategoryAsync(MealCategoryDto category);
        Task UpdateMealCategoryAsync(MealCategoryUpdateDto category);
        Task DeleteMealCategoryByIdAsync(int categoryId);
    }
}
