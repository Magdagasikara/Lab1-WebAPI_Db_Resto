
namespace Lab1_WebAPI_Db_Resto.Models
{
    public class MealCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryOrder { get; set; }
        //virtual makes it load lazily ie only if we want to use Meals explicitly. Thats why no virtual here.
        public ICollection<Meal>? Meals { get; set; }
    }
}
