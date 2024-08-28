namespace Lab1_WebAPI_Db_Resto.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public int Price { get; set; } // no decimals in Sweden 

        public virtual ICollection<MealIngredient>? MealIngredient { get; set; }
        public virtual ICollection<MealCategory>? MealCategories { get; set; }
    }
}
