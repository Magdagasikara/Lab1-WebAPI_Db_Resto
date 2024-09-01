using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs
{
    public class BookingDto
    {
        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public int AmountOfGuests { get; set; }
        [Required]
        public DateTime ReservationStart { get; set; }
        [Required]
        public double ReservationDurationInHours{ get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please fill in correct Email-address")]
        [StringLength(100, MinimumLength = 6)]
        public string Email { get; set; }
    }
}
