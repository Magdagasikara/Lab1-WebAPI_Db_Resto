using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs.MealCategory
{
    public class MealCategoryDto
    {
        [Required]
        //[StringLength(100, MinimumLength = 5, ErrorMessage = "Enter name of the dish")]
        public string Name { get; set; }

        public int CategoryOrder { get; set; }
    }
}
