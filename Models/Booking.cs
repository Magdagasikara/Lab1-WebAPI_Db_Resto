using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1_WebAPI_Db_Resto.Models
{
    [Index(nameof(BookingNumber), IsUnique = true)]
    public class Booking
    {
        public int Id { get; set; }
        [Required]
        public string BookingNumber { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public int AmountOfGuests { get; set; }
        [Required]
        public DateTime ReservationStart { get; set; }
        [Required]
        public DateTime ReservationEnd { get; set; } //Dto will have duration instead
        public ICollection<TableBooking> TableBookings { get; set; }
        [ForeignKey("Customer")]
        public int FK_CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
