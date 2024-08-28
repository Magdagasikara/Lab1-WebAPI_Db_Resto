using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1_WebAPI_Db_Resto.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime TimeStamp{ get; set; }
        public int AmountOfGuests { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; } //Dto will have duration instead
        public bool IsActive { get; set; }

        //[ForeignKey("TableBooking")]
        //public int FK_TableBookingId { get; set; }
        public ICollection<TableBooking> TableBookings { get; set; }
        [ForeignKey("Customer")]
        public int FK_CustomerId { get; set; }
        public /*virtual*/ Customer Customer { get; set; }
    }
}
