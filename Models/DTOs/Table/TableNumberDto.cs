using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs.Table
{
    public class TableNumberDto
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Table number must be between 1 and 1000.")]
        public int TableNumber { get; set; }
    }
}
