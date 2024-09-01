namespace Lab1_WebAPI_Db_Resto.Models.ViewModels
{
    public class MealListVM
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public int Price { get; set; } // no decimals in Sweden 
    }
}
