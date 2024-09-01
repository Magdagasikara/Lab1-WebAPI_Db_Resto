using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public interface IMealServices
    {
        Task<IEnumerable<MealListVM>> GetAllMealsAsync();
        Task<MealListVM> GetMealByIdAsync(int mealId);
        Task AddMealAsync(MealDto meal);
        Task UpdateMealAsync(MealUpdateDto meal);
        Task DeleteMealByIdAsync(int mealId);
    }
}
