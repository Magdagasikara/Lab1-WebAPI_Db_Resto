using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs.MealCategory
{
    public class MealCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryOrder { get; set; }
    }
}
