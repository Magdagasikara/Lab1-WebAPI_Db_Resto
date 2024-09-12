using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly RestoContext _context;
        public MealRepository(RestoContext context)
        {
            _context = context;
        }

        public async Task AddMealAsync(Meal meal)
        {
            try
            {
                await _context.Meals.AddAsync(meal);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when adding a new meal", ex);
            }
        }

        public async Task DeleteMealByIdAsync(int mealId)
        {
            try
            {
                var meal = await GetMealByIdAsync(mealId);
                _context.Meals.Remove(meal);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when deleting a meal", ex);
            }
        }

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            try
            {
                return await _context.Meals
                    .Include(m => m.MealCategory)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of meals", ex);
            }
        }

        public async Task<Meal> GetMealByIdAsync(int mealId)
        {
            try
            {
                var meal = await _context
                    .Meals
                    .Include(m => m.MealCategory)
                    .SingleOrDefaultAsync(m => m.Id == mealId);
                //.FindAsync(mealId);
                if (meal != null)
                {
                    return meal;
                }
                throw new KeyNotFoundException($"Meal with id {mealId} not found");
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

        public async Task<IEnumerable<Meal>> GetMealsByCategoryAsync(MealCategory category)
        {
            // meals should always follow when getting a category

            try
            {
                return await _context.Meals
                    .Where(mc => mc.MealCategory == category)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of meals", ex);
            }
        }

        public async Task UpdateMealAsync(Meal updatedMeal)
        {
            if (updatedMeal == null)
            {
                throw new ArgumentNullException(nameof(updatedMeal), "No meal to be updated");
            }
            try
            {
                var meal = await GetMealByIdAsync(updatedMeal.Id);
                meal.Name = updatedMeal.Name;
                meal.Description = updatedMeal.Description;
                meal.Price = updatedMeal.Price;
                meal.IsAvailable = updatedMeal.IsAvailable;
                meal.FK_MealCategoryId = updatedMeal.FK_MealCategoryId;
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Meal to be updated does not exist in Db");
            }
            catch (Exception ex)
            {
                throw new Exception("Error when updating a meal", ex);
            }
        }
    }
}
