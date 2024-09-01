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
                return await _context.Meals.ToListAsync();
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
                var meal = await _context.Meals.FindAsync(mealId);
                if (meal != null)
                {
                    return meal;
                }
                throw new KeyNotFoundException($"MealId {mealId} does not return a customer");
            }
            catch (Exception ex)
            {
                throw new Exception("Error when getting a meals", ex);
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
                await _context.SaveChangesAsync();
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
