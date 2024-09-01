using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1_WebAPI_Db_Resto.Models
{
    public class MealIngredient
    {
        public int Id { get; set; }
        //[ForeignKey("Ingredient")]
        //public int FK_IngredientId { get; set; }
        //public Ingredient Ingredient { get; set; }
        //[ForeignKey("Meal")]
        //public int FK_MealId { get; set; }
        //public Meal Meal { get; set; }
    }
}
