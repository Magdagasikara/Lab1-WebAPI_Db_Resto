using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models
{
    [Index(nameof(TableNumber), IsUnique = true)]
    public class Table
    {
        public int Id{ get; set; }
        [Required]
        [Range(1, 1000)]
        public int TableNumber { get; set; }
        [Required]
        [Range(0, 1000)]
        public int AmountOfPlaces { get; set; }

        public virtual ICollection<TableBooking>? TableBooking { get; set; }
    }
}
