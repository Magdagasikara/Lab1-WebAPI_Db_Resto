using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs.Table
{
    public class TableUpdateDto
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Table number must be between 1 and 1000.")]
        public int TableNumber { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Amount of places must be between 0 and 1000.")]
        public int AmountOfPlaces { get; set; }
        [Range(1, 1000)]
        public int? UpdatedTableNumber { get; set; }
    }
}
