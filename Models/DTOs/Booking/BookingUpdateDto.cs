namespace Lab1_WebAPI_Db_Resto.Models.DTOs.Booking
{
    public class BookingUpdateDto
    {
        public string BookingNumber { get; set; }
        public DateTime TimeStamp { get; set; }
        public int AmountOfGuests { get; set; }
        public DateTime ReservationStart { get; set; }
        public double ReservationDurationInHours { get; set; }
        public string Email { get; set; }
    }
}
