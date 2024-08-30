using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs
{
    public class BookingNumberDto
    {
        [Required]
        public int BookingNumber { get; set; }
    }
}
