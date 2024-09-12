using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs.MealCategory
{
    public class MealCategoryUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
