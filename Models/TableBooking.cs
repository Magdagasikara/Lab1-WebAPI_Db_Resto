using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1_WebAPI_Db_Resto.Models
{
    public class TableBooking
    {
        public int Id { get; set; }
        [ForeignKey("Table")]
        public int FK_TableId { get; set; }
        public Table Table { get; set; }
        [ForeignKey("Booking")]
        public int FK_BookingId { get; set; }
        public Booking Booking { get; set; }


    }
}
