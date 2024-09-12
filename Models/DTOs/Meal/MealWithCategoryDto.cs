using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs.Meal
{
    public class MealWithCategoryDto
    {
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Enter name of the dish")]
        public string Name { get; set; }
        [StringLength(200)]
        public string? Description { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        [Range(25, 1000, ErrorMessage = "Enter price in SEK, it must be between 25 and 1000 kr")]
        public int Price { get; set; }
        public int? FK_MealCategoryId { get; set; }
    }
}
