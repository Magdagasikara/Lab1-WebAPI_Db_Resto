using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories
{
    public class MealCategoryRepository : IMealCategoryRepository
    {
        private readonly RestoContext _context;
        public MealCategoryRepository(RestoContext context)
        {
            _context = context;
        }

        public async Task AddMealCategoryAsync(MealCategory category)
        {
            try
            {
                await _context.MealCategories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when adding a new meal category", ex);
            }

        }

        public async Task DeleteMealCategoryByIdAsync(int categoryId)
        {

            try
            {
                var mealCategory = await GetMealCategoryByIdAsync(categoryId);
                if (mealCategory.Meals is not null)
                {
                    foreach (var meal in mealCategory.Meals)
                    {
                        meal.FK_MealCategoryId = null;
                    }
                }
                _context.MealCategories.Remove(mealCategory);
                await _context.SaveChangesAsync();

            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage = $"(inner Exception) {ex.InnerException.Message}";
                }
                throw new Exception($"Error in repo: {errorMessage}");
            }
        }

        public async Task<IEnumerable<MealCategory>> GetAllMealCategoriesAsync()
        {
            try
            {
                return await _context
                    .MealCategories
                    .OrderBy(mc => mc.CategoryOrder)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of meal categories", ex);
            }
        }
        
        public async Task<MealCategory> GetMealCategoryByIdAsync(int categoryId)
        {
            try
            {
                var mealCategory = await _context
                    .MealCategories
                    .Include(mc => mc.Meals)
                    .SingleOrDefaultAsync(mc => mc.Id == categoryId);

                if (mealCategory != null)
                {
                    return mealCategory;
                }
                throw new KeyNotFoundException($"Meal category with id {categoryId} not found");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when getting a meals", ex);
            }
        }

        public async Task UpdateMealCategoryAsync(MealCategory updatedCategory)
        {
            try
            {
                var mealCategory = await GetMealCategoryByIdAsync(updatedCategory.Id);
                mealCategory.Name = updatedCategory.Name;
                //mealCategory.Meals = updatedCategory.Meals;
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Meal category to be updated does not exist in Db");
            }
            catch (Exception ex)
            {
                throw new Exception("Error when updating a meal category", ex);
            }
        }
    }
}
