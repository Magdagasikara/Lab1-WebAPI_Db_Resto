using System.ComponentModel.DataAnnotations;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Meal;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs.MealCategory
{
    public class MealCategoryWithMealsDto
    {
        [Required]
        //[StringLength(100, MinimumLength = 5, ErrorMessage = "Enter name of the dish")]
        public string Name { get; set; }
        public ICollection<MealDto>? Meals { get; set; }

    }
}
