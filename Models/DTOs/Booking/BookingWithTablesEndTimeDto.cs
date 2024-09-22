using Lab1_WebAPI_Db_Resto.Models.DTOs.Table;

namespace Lab1_WebAPI_Db_Resto.Models.DTOs.Booking
{
    public class BookingWithTablesEndTimeDto
    {
        public string BookingNumber { get; set; }
        public string Email { get; set; }
        public DateTime TimeStamp { get; set; }
        public int AmountOfGuests { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }

        public ICollection<TableDto>? Tables { get; set; }
    }
}
