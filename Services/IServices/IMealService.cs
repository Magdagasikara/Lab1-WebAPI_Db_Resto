using Lab1_WebAPI_Db_Resto.Models.DTOs.Meal;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface IMealService
    {
        Task<IEnumerable<MealWithCategoryDto>> GetAllMealsAsync();
        Task<MealWithCategoryDto> GetMealByIdAsync(int mealId);
        Task AddMealAsync(MealWithCategoryDto meal);
        Task UpdateMealAsync(MealUpdateDto meal);
        Task DeleteMealByIdAsync(int mealId);
    }
}
