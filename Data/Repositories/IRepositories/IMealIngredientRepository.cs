using Lab1_WebAPI_Db_Resto.Models;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories
{
    public interface IMealIngredientRepository
    {
        Task AddIngredientsToMealAsync(Meal meal, List<Ingredient> ingredients);
        Task RemoveIngredientsFromMealAsync(Meal meal, List<Ingredient> ingredients);
        Task<IEnumerable<Ingredient>> GetIngredientsByMealAsync(Meal meal);
        Task<IEnumerable<Meal>> GetMealsByIngredientAsync(Ingredient ingredient);
        Task UpdateMealsAvailabilityByIngredientAsync(Ingredient ingredient);
    }
}
