using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs.Booking
{
    public class BookingDto
    {
        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Amount of guests must be minimum 1")]
        public int AmountOfGuests { get; set; }
        [Required]
        public DateTime ReservationStart { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Reservation time must be minimum 1 hour")]
        public double ReservationDurationInHours { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please fill in correct e-mail address")]
        [StringLength(100, MinimumLength = 6)]
        public string Email { get; set; }
    }
}
