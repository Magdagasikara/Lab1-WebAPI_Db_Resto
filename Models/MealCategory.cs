
namespace Lab1_WebAPI_Db_Resto.Models
{
    public class MealCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //virtual makes it load lazily ie only if we want to use Meals explicitly
        public virtual ICollection<Meal>? Meals { get; set; }
    }
}
