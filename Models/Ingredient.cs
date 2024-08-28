namespace Lab1_WebAPI_Db_Resto.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }


        public virtual ICollection<MealIngredient>? MealIngredient{ get; set; }
    }
}
