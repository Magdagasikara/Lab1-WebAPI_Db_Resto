using Lab1_WebAPI_Db_Resto.Models;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories
{
    public interface IMealRepository
    {
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        //Task<IEnumerable<Meal>> GetMealsByCategoryAsync(MealCategory category);
        Task<Meal> GetMealByIdAsync(int mealId);
        Task AddMealAsync(Meal meal);
        Task UpdateMealAsync(Meal meal);
        Task DeleteMealByIdAsync(int mealId);
    }
}
