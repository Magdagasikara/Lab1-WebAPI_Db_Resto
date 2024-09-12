using Lab1_WebAPI_Db_Resto.Models;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories
{
    public interface IMealCategoryRepository
    {
        Task<IEnumerable<MealCategory>> GetAllMealCategoriesAsync();
        Task<MealCategory> GetMealCategoryByIdAsync(int categoryId);
        Task AddMealCategoryAsync(MealCategory category);
        Task UpdateMealCategoryAsync(MealCategory category);
        Task DeleteMealCategoryByIdAsync(int categoryId);
    }
}
